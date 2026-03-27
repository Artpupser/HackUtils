using Microsoft.EntityFrameworkCore;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public sealed class LevelModel {
   public LevelEntity LevelEntity { get; }

   private LevelModel(LevelEntity levelEntity) {
      LevelEntity = levelEntity;
   }

   public static async Task<LevelModel?> LoadFromId(int id, CancellationToken cancellationToken) {
      var result = await RyazanTripApp.Instance.Context.LevelsSet.FirstOrDefaultAsync(x => x.LevelId == id,
         cancellationToken);
      return result == null ? null : new LevelModel(result);
   }
}