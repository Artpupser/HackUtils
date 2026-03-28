using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Extensions;

public static class EntityExtensions {
   public static async Task<bool> CreateSession(this UserEntity userEntity, ISession session,
      CancellationToken cancellationToken) {
      var sessionEntity = new SessionEntity {
         UserId = userEntity.Id,
         Token = session.Id,
         CreatedAt = DateTime.UtcNow,
         ExpiresAt = DateTime.UtcNow.AddDays(1)
      };
      await RyazanTripApp.Instance.Context.SessionsSet.AddAsync(sessionEntity, cancellationToken);
      var countSaved = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
      return countSaved > 0 && await sessionEntity.CheckAnySession(cancellationToken);
   }

   public static async Task<bool> CheckAnyUser(this UserEntity entity, CancellationToken cancellationToken) {
      return await RyazanTripApp.Instance.Context.UsersSet.AnyAsync(x => x.Email == entity.Email,
         cancellationToken);
   }

   public static async Task<bool> CheckAnySession(this SessionEntity entity, CancellationToken cancellationToken) {
      return await RyazanTripApp.Instance.Context.SessionsSet.AnyAsync(x => x.Token == entity.Token,
         cancellationToken);
   }
}