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
   public DateOnly Date { get; set; }

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("coords_list_str")]
   public string CoordsListStr { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("image_base64")]
   public string ImageBase64 { get; set; } = string.Empty;

   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("time")]
   public TimeOnly Time { get; set; }
   
   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("can_children")]
   public bool CanChildren { get; set; }
   
   [ValidRule(ValidRuleType.Need)]
   [JsonPropertyName("can_dogs")]
   public bool CanDogs { get; set; }

   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.Min, 0)]
   [JsonPropertyName("price")]
   public decimal Price { get; set; }
}

public sealed class TourController : Controller {
   [ControllerHandler("/api/tours/create", HttpMethodType.POST)]
   public async Task CreateTourHandler(Request request, Response response, CancellationToken cancellationToken) {
      try {
         var user = await UserModel.LoadUserFromRequest(request.Session!, cancellationToken);
         if (user != null || !user.CheckAdmin()) {
            response.ErrorStack.PushStack("You don't have permissions to create tour");
            await response.SendAsync(cancellationToken);
            return;
         }
         var content = await request.ReadContentT<CreateTourRequest>(cancellationToken);
         if (await ValidatorManager.Valid(request, response, content, cancellationToken)) {
            WebApp.SecureContextInstance.Logger.LogInformation("Create tour handler invoked, {Title}", content.Name);
         
            var tourEntity = new TourEntity {
               Title = content.Name,
               Description = content.Description,
               Price = content.Price,
               TourTime = content.Date.ToDateTime(content.Time),
               Coords = content.CoordsListStr,
               
            };
         
            ImageModel? image = null;
            var base64Data = content.ImageBase64.Split(',')[1];
            image = await ImageModel.UploadAndGetFromRequestAsync(new ImageRequest() {
               ImageBase64 = base64Data
            }, cancellationToken);

            tourEntity.ImageId = image.ImageEntity.ImageId;
         
            await RyazanTripApp.Instance.Context.ToursSet.AddAsync(tourEntity, cancellationToken);
            var changes = await RyazanTripApp.Instance.Context.SaveChangesAsync(cancellationToken);
            await response.SendSuccess(changes > 0, cancellationToken);
         }
      } catch (Exception e) {
         WebApp.SecureContextInstance.Logger.LogError("Create tour handler error: {E}", e);
         response.ErrorStack.PushStack("Failed to create tour");
      }
   }
}