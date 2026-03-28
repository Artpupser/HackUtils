using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Core;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public sealed class UserModel {
   public UserEntity UserEntity { get; }
   public SessionEntity SessionEntity { get; }

   private UserModel(UserEntity userEntity, SessionEntity sessionEntity) {
      UserEntity = userEntity;
      SessionEntity = sessionEntity;
   }

   public static async Task<UserModel?> LoadUserFromRequest(ISession session, CancellationToken cancellationToken) {
      var sessionEntity = await RyazanTripApp.Instance.Context.SessionsSet.FirstOrDefaultAsync(x => x.Token == session.Id, cancellationToken: cancellationToken);
      if (sessionEntity != null) {
         var userEntity = await RyazanTripApp.Instance.Context.UsersSet.FirstOrDefaultAsync(x => x.Id == sessionEntity.UserId, cancellationToken: cancellationToken);
         if (userEntity != null) {
            return new UserModel(userEntity, sessionEntity);
         }
      }
      return null;
   }

   public static async Task<UserModel?> IncludeUserFromRequest(ISession session, CancellationToken cancellationToken) {
      var sessionEntity = await RyazanTripApp.Instance.Context.SessionsSet.FirstOrDefaultAsync(x => x.Token == session.Id, cancellationToken: cancellationToken);
      if (sessionEntity != null) {
         var userEntity = await RyazanTripApp.Instance.Context.UsersSet
            .Include(u => u.LevelEntity)
            .Include(u => u.RoleEntity)
            .Include(u => u.UserTours)
            .ThenInclude(ut => ut.TourEntity)
            .ThenInclude(t => t.ImageEntity)
            .FirstOrDefaultAsync(x => x.Id == sessionEntity.UserId, cancellationToken: cancellationToken);
         if (userEntity != null) {
            return new UserModel(userEntity, sessionEntity);
         }
      }
      return null;
   }

   public bool Check() => SessionEntity.VerifyExpires();
   public bool CheckAdmin() => SessionEntity.VerifyExpires() && UserEntity.RoleId > 1;
}