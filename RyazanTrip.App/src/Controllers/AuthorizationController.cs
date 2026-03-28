using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

using RyazanTrip.App.Extensions;
using RyazanTrip.App.Models;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Controllers;

public record LogoutRequest : LoginRequest;

public record LoginRequest() {
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 3)]
   [ValidRule(ValidRuleType.MaxLength, 24)]
   [ValidRule(ValidRuleType.Email)]
   [JsonPropertyName("email")]
   public string Email { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 5)]
   [ValidRule(ValidRuleType.MaxLength, 64)]
   [JsonPropertyName("password")]
   public string Password { get; set; } = string.Empty;
}

public record SuccessResponse() {
   [JsonPropertyName("success")] public bool Success { get; set; }
}

public record RegistrationRequest() {
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 3)]
   [ValidRule(ValidRuleType.MaxLength, 24)]
   [JsonPropertyName("username")]
   public string Username { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.Email)]
   [JsonPropertyName("email")]
   public string Email { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 3)]
   [ValidRule(ValidRuleType.MaxLength, 32)]
   [JsonPropertyName("town")]
   public string Town { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 5)]
   [ValidRule(ValidRuleType.MaxLength, 64)]
   [JsonPropertyName("password")]
   public string Password { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 5)]
   [ValidRule(ValidRuleType.MaxLength, 32)]
   [JsonPropertyName("password_repeat")]
   public string PasswordRepeat { get; set; } = string.Empty;
}

public class AuthorizationController : Controller {
   [ControllerHandler("/api/login", HttpMethodType.POST)]
   private async Task LoginHandler(Request request, Response response, CancellationToken cancellationToken) {
      var content = await request.ReadContentT<LoginRequest>(cancellationToken);
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
         WebApp.SecureContextInstance.Logger.LogInformation("Login handler invoked, {Email}", content.Email);
         content.Password = CryptoUtils.ComputeSha256Hash(content.Password);
         var user = await RyazanTripApp.Instance.Context.UsersSet.Include(userEntity => userEntity.Sessions).FirstOrDefaultAsync(x => x.Email == content.Email, cancellationToken);
         if (user != null && user.PasswordHash == content.Password) {
            var sessionResult = await user.RegenerateSessions(request, cancellationToken);
            await  response.SendSuccess(sessionResult, cancellationToken);
            return;
         }
         response.ErrorStack.PushStack("Password or email not correct");
      }
   }

   [ControllerHandler("/api/registration", HttpMethodType.POST)]
   private async Task RegistrationHandler(Request request, Response response, CancellationToken cancellationToken) {
      var content = await request.ReadContentT<RegistrationRequest>(cancellationToken);
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
         if (content.Password == content.PasswordRepeat) {
            content.Password = CryptoUtils.ComputeSha256Hash(content.Password);
            var user = new UserEntity {
               Email = content.Email,
               Username = content.Username,
               Town = content.Town,
               PasswordHash = content.Password,
               LevelId = 1,
               RoleId = 1,
               Experience = 0,
            };
            if (await user.CheckAnyUser(cancellationToken)) {
               response.ErrorStack.PushStack($"User with {user.Email}  already created");
               return;
            }
            await RyazanTripApp.Instance.Context.UsersSet.AddAsync(user, cancellationToken);
            var countSaved = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
            WebApp.SecureContextInstance.Logger.LogInformation("Saved!, {Email}", content.Email);
            
            if (countSaved > 0) {
               var savedUser = await RyazanTripApp.Instance.Context.UsersSet
                  .Include(u => u.Sessions)
                  .FirstOrDefaultAsync(x => x.Email == content.Email, cancellationToken);
               
               if (savedUser != null && await savedUser.RegenerateSessions(request, cancellationToken)) {
                  await response.SendSuccess(true, cancellationToken);
                  return;
               }
            }
            response.ErrorStack.PushStack("Error user creation");
         }

         response.ErrorStack.PushStack("Passwords not equals");
      }
   }
   


   [ControllerHandler("/api/logout", HttpMethodType.POST)]
   private async Task LogoutHandler(Request request, Response response, CancellationToken cancellationToken) {
      var content = await request.ReadContentT<LogoutRequest>(cancellationToken);
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
         var userModel = await UserModel.LoadUserFromRequest(request.Session!, cancellationToken);
         content.Password = CryptoUtils.ComputeSha256Hash(content.Password);
         if (userModel != null && userModel.UserEntity.Email == content.Email && userModel.UserEntity.PasswordHash == content.Password) {
            var session = userModel.UserEntity.Sessions.FirstOrDefault(s => s.Token == request.Session!.Id);
            if (session != null) {
               RyazanTripApp.Instance.Context.SessionsSet.Remove(session);
               var countSaved = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
               if (countSaved > 0) {
                  await response.SendSuccess(true, cancellationToken);
                  return;
               }
               response.ErrorStack.PushStack("Error session remove");
            } else {
               response.ErrorStack.PushStack("Session not found");
            }
            return;  
         } 
         response.ErrorStack.PushStack("Email or password not correct");
      }
   }
}