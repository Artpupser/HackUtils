using System.Text.Json.Serialization;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

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
   public string? ImageBase64 { get; set; }

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
      
   }
}