using Microsoft.AspNetCore.Http;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Models;

public sealed class UserModel {
   private UserEntity _userEntity;
   private SessionEntity _sessionEntity;   
   private UserModel(UserEntity userEntity, SessionEntity sessionEntity) {
      _userEntity = userEntity;
      _sessionEntity = sessionEntity;
   }

   public async Task<UserModel?> LoadUserFromRequest(ISession session) {
      var sessionEntity = RyazanTripApp.Instance.Context.SessionsSet.FirstOrDefault(x => x.Token == session.Id);
      if (sessionEntity != null) {
         var userEntity = RyazanTripApp.Instance.Context.UsersSet.FirstOrDefault(x => x.Id == sessionEntity.UserId);
         if (userEntity != null) {
            return new UserModel(userEntity, sessionEntity);
         }
      }
      return null;
   }
}