using System.Text.Json.Serialization;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

using RyazanTrip.App.Extensions;
using RyazanTrip.App.Models;
using RyazanTrip.App.Procedures;
using RyazanTrip.DataAccess.Postgres.Entities;

namespace RyazanTrip.App.Controllers;

public record CreateTourRequest() 
{
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 3)]
   [ValidRule(ValidRuleType.MaxLength, 128)]
   [JsonPropertyName("name")]
   public string Name { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 10)]
   [ValidRule(ValidRuleType.MaxLength, 2000)]
   [JsonPropertyName("description")]
   public string Description { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("date")]
   public DateTime Date { get; set; }

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("coords_list_str")]
   public string CoordsListStr { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("image")]
   public string ImageBase64 { get; set; } = string.Empty;
   

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.Min, 1)]
   [ValidRule(ValidRuleType.Max, int.MaxValue)]
   [JsonPropertyName("price")]
   public float Price { get; set; }
}

public sealed class TourController : Controller {
   [ControllerHandler("/api/tours/create", HttpMethodType.POST)]
   private async Task CreateTourHandler(Request request, Response response, CancellationToken cancellationToken) {
      try {
         var userModel = await UserModel.LoadUserFromRequest(request.Session!, cancellationToken);
         WebApp.SecureContextInstance.Logger.LogInformation("Create tour handler invoked by user {UserId}", userModel?.UserEntity.Id);
         if (userModel == null || !userModel.CheckAdmin()) {
            response.ErrorStack.PushStack("You don't have permissions to create tour");
            WebApp.SecureContextInstance.Logger.LogInformation("Not admin");
            await response.SendAsync(cancellationToken);
            return;
         }
         var content = await request.ReadContentT<CreateTourRequest>(cancellationToken);
         WebApp.SecureContextInstance.Logger.LogInformation("Read tour creation request content: {Content}", content);
         if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
            WebApp.SecureContextInstance.Logger.LogInformation("Create tour handler invoked, {Title}", content.Name);
         
            var tourEntity = new TourEntity {
               Title = content.Name,
               Description = content.Description,
               Price = content.Price,
               TourTime = content.Date,
               Coords = content.CoordsListStr,
            };
         
            ImageModel? image = null;
            var base64Data = content.ImageBase64.Split(',')[1];
            image = await ImageModel.UploadAndGetFromRequestAsync(new ImageRequest() {
               ImageBase64 = base64Data
            }, cancellationToken);
            tourEntity.ImageId = image.ImageEntity.ImageId;

            WebApp.SecureContextInstance.Logger.LogInformation("Image uploaded, got image model: {ImageModel}", image.ImageEntity.Url);
         
            await RyazanTripApp.Instance.Context.ToursSet.AddAsync(tourEntity, cancellationToken);
            WebApp.SecureContextInstance.Logger.LogInformation("Added");
            var changes = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
            WebApp.SecureContextInstance.Logger.LogInformation("Saved, changes count: {Changes}", changes);
            await response.SendSuccess(changes > 0, cancellationToken);
         } else {
            foreach (var s in response.ErrorStack.StackContentList) {
               WebApp.SecureContextInstance.Logger.LogError("StackError: {E}", s);
            }
         }
      } catch (Exception e) {
         WebApp.SecureContextInstance.Logger.LogError("Create tour handler error: {E}", e);
         response.ErrorStack.PushStack("Failed to create tour");
      }
   }
}