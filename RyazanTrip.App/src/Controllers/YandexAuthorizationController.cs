using System.Data.Entity;
using System.Security.Policy;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;

using RyazanTrip.App.Extensions;
using RyazanTrip.App.Models;
using RyazanTrip.App.Procedures;
using RyazanTrip.App.Services;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Controllers;

public class YandexAuthorizationController : Controller {
   [ControllerHandler("/api/yandex/callback", HttpMethodType.GET)]
   private async Task YandexCallbackHandler(Request request, Response response, CancellationToken cancellationToken) {
      try {
         var code = request.GetQueryValue("code");
         if (code == string.Empty) {
            response.ErrorStack.PushStack("Code is empty");
         }

         var result = await RyazanTripApp.Instance.YandexMicroMicroService.ExchangeCodeAsync(code, cancellationToken);
         var userEntity = new UserEntity {
            Email = result.Email,
            Username = result.Login,
            Town = "Рязань",
            PasswordHash = CryptoUtils.ComputeSha256Hash(Random.Shared.Next(0,int.MaxValue).ToString()),
            LevelId = 1,
            RoleId = 1,
            Experience = 0,
         };
         WebApp.SecureContextInstance.Logger.LogWarning("User entity created...");
         if (await userEntity.CheckAnyUser(cancellationToken)) {
            response.ErrorStack.PushStack($"User with {userEntity.Email}  already created");
            response.Redirect("/authorization");
            await response.SendAsync(cancellationToken);
            return;
         }

         WebApp.SecureContextInstance.Logger.LogWarning("Checked");
         
         await RyazanTripApp.Instance.Context.UsersSet.AddAsync(userEntity, cancellationToken);
         var countSaved = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);

         WebApp.SecureContextInstance.Logger.LogWarning("Saving...");
         if (countSaved > 0) {
            WebApp.SecureContextInstance.Logger.LogWarning("Successfully saved!");
            var savedUser = RyazanTripApp.Instance.Context.UsersSet.FirstOrDefault(x => x.Email == result.Email);
            if (savedUser != null && await savedUser.RegenerateSessions(request, cancellationToken)) {
               response.Redirect("/profile");
               await response.SendAsync(cancellationToken);
            }
         }
      } catch (Exception e) {
         response.ErrorStack.PushStack($"User with {e}  already created");
         WebApp.SecureContextInstance.Logger.LogError("Error in Yandex callback: {Message}", e);
         response.Redirect("/authorization");
         await response.SendAsync(cancellationToken);
      }
   }
}