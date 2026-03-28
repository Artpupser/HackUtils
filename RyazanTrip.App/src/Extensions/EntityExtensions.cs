using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Core;

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
   
   public static async Task<bool> RegenerateSessions(this UserEntity user, Request request, CancellationToken cancellationToken) {
      var session = user.Sessions.FirstOrDefault(s => s.Token == request.Session!.Id);
      WebApp.SecureContextInstance.Logger.LogInformation("Regeneration session, {SessionId}", request.Session!.Id);
      if (session == null) {
         session = user.Sessions.FirstOrDefault(s => s.UserId == user.Id);
         if (session == null) {
            return await user.CreateSession(request.Session!, cancellationToken);
         }
         session.Token = request.Session.Id;
         return await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken) > 0;
      }
      session.ExpiresAt = DateTime.UtcNow.AddDays(1);
      return await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken) > 0;
   }
}