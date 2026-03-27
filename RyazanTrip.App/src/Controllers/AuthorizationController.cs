using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Controllers;

public record LogoutRequest : LoginRequest;

public record LoginRequest() {
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 3)]
   [ValidRule(ValidRuleType.MaxLength, 24)]
   [JsonPropertyName("username")]
   public string Username { get; set; } = string.Empty;

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
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) { }

      await Task.CompletedTask;
   }

   [ControllerHandler("/api/registration", HttpMethodType.POST)]
   private async Task RegistrationHandler(Request request, Response response, CancellationToken cancellationToken) {
      var content = await request.ReadContentT<RegistrationRequest>(cancellationToken);
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
         content.Password = CryptoUtils.ComputeSha256Hash(content.Password);
         content.PasswordRepeat = CryptoUtils.ComputeSha256Hash(content.PasswordRepeat);
         if (content.Password == content.PasswordRepeat) {
            var user = new UserEntity {
               Email = content.Email,
               Username = content.Username,
               PasswordHash = content.Password
            };
            if (await user.CheckAnyUser(cancellationToken)) {
               response.ErrorStack.PushStack($"User with {user.Email}  already created");
               return;
            }

            var entry = await RyazanTripApp.Instance.Context.UsersSet.AddAsync(user, cancellationToken);
            var countSaved = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
            if (countSaved > 0 && await user.CheckAnyUser(cancellationToken) &&
                await user.CreateSession(request.Session!, cancellationToken)) {
               var successResponse = new SuccessResponse {
                  Success = true
               };
               response.WriteTJsonToCache(successResponse);
               await response.SendAsync(cancellationToken);
            }

            response.ErrorStack.PushStack("Error user creation");
         }

         response.ErrorStack.PushStack("Passwords not equals");
      }

      await Task.CompletedTask;
   }

   [ControllerHandler("/api/logout", HttpMethodType.POST)]
   private async Task LogoutHandler(Request request, Response response, CancellationToken cancellationToken) {
      var content = await request.ReadContentT<LogoutRequest>(cancellationToken);
      if (await ValidatorManager.Valid(request, response, content, cancellationToken)) { }

      await Task.CompletedTask;
   }
}

public static class EntityExtensions {
   public static async Task<bool> CreateSession(this UserEntity userEntity, ISession session,
      CancellationToken cancellationToken) {
      var sessionEntity = new SessionEntity {
         UserId = userEntity.UserId,
         Token = session.Id,
         CreatedAt = DateTime.Now,
         ExpiresAt = DateTime.Now.AddDays(1)
      };
      var entry = await RyazanTripApp.Instance.Context.SessionsSet.AddAsync(sessionEntity, cancellationToken);
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

public sealed class SessionUser {
   public SessionUser() { }
}