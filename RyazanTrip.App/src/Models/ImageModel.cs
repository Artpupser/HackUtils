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
   
   public async Task<ImageModel> LoadFromRequestAsync(ImageRequest request, CancellationToken cancellationToken) {
      var guid = CryptoUtils.ComputeSha256Hash(Guid.NewGuid().ToString());
      var s3 = new S3Procedure(WebApp.);
      var imageEntity = new ImageEntity {
         Url = 
      };
      RyazanTripApp.Instance.Context.ImagesSet.Add(imageEntity);
      await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
      return new ImageModel(imageEntity);
   }

   public async Task<ImageModel?> GetFromIdAsync(int id, CancellationToken cancellationToken) {
      var imageEntity = await RyazanTripApp.Instance.Context.ImagesSet.FirstOrDefaultAsync(x => x.ImageId == id, cancellationToken);
      return imageEntity == null ? null : new ImageModel(imageEntity);
   }
}
