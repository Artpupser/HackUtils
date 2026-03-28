using Microsoft.EntityFrameworkCore;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public record ImageRequest {
}

public sealed class ImageModel {
   public ImageEntity ImageEntity { get; set; }

   private ImageModel(ImageEntity imageEntity) {
      ImageEntity = imageEntity;
   }

   public async Task<ImageModel?> GetFromIdAsync(int id, CancellationToken cancellationToken) {
      var imageEntity = await RyazanTripApp.Instance.Context.ImagesSet.FirstOrDefaultAsync(x => x.ImageId == id, cancellationToken);
      return imageEntity == null ? null : new ImageModel(imageEntity);
   }
}