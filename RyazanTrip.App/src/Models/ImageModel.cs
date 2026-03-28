using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

using RyazanTrip.App.Procedures;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public record ImageRequest {
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.Min, 128)]
   public string ImageBase64 { get; set; } = null!;
}

public sealed class ImageModel {
   public ImageEntity ImageEntity { get; set; }

   private ImageModel(ImageEntity imageEntity) {
      ImageEntity = imageEntity;
   }
   
   public static async Task<ImageModel?> UploadAndGetFromRequestAsync(ImageRequest request, CancellationToken cancellationToken) {
      var uploadResult = await UploadFromRequestAsync(request, cancellationToken);
      if (!uploadResult) return null;
      var imageEntity = await RyazanTripApp.Instance.Context.ImagesSet.FirstOrDefaultAsync(x => x.Url == request.ImageBase64, cancellationToken);
      return imageEntity == null ? null : new ImageModel(imageEntity);
   }
   
   public static async Task<bool> UploadFromRequestAsync(ImageRequest request, CancellationToken cancellationToken) {
      var guid = CryptoUtils.ComputeSha256Hash(Guid.NewGuid().ToString());
      using var s3 = new S3Procedure(RyazanTripApp.S3);
      var result = await s3.UploadImageBytesAsync(Convert.FromBase64String(request.ImageBase64), guid, cancellationToken);
      if (!result.Success) return false;
      var imageEntity = new ImageEntity {
         Url = result.Url
      };
      RyazanTripApp.Instance.Context.ImagesSet.Add(imageEntity);
      var countChanges = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
      return countChanges > 0;
   }

   public static async Task<ImageModel?> GetFromIdAsync(int id, CancellationToken cancellationToken) {
      var imageEntity = await RyazanTripApp.Instance.Context.ImagesSet.FirstOrDefaultAsync(x => x.ImageId == id, cancellationToken);
      return imageEntity == null ? null : new ImageModel(imageEntity);
   }
}
