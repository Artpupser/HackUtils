using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Controllers;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public sealed class TourModel : Controller {
   private TourEntity? _tourEntity;
   private TourModel(TourEntity tourEntity) {
      _tourEntity = tourEntity;
   }
   public async Task<TourModel?> GetTourByIdAsync(int tourId, CancellationToken cancellationToken) {
      var tourEntity =
         await RyazanTripApp.Instance.Context.ToursSet.FirstOrDefaultAsync(x => x.TourId == tourId,
            cancellationToken: cancellationToken);
      return tourEntity == null ? null : new TourModel(tourEntity);
   }
   
   public async Task<List<TourModel>> GetToursByUserAsync(UserModel model, CancellationToken cancellationToken) {
      var list = new List<TourModel>();
      foreach (var userTour in model.UserEntity.UserTours) {
         var tour = await GetTourByIdAsync(userTour.TourId, cancellationToken);
         if (tour == null) {
            continue;
         }
         list.Add(tour);
      }
      return list;
   }
}