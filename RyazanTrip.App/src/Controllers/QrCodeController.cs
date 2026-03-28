using System.Text.Json.Serialization;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Validations;

using RyazanTrip.App.Extensions;

namespace RyazanTrip.App.Controllers;

public record QrcodeRequest() {
   [ValidRule(ValidRuleType.Need)]
   [ValidRule(ValidRuleType.MinLength, 64)]
   [ValidRule(ValidRuleType.MaxLength, 128)]
   [JsonPropertyName("tour_encode_id")]
   public string TourEncodeId { get; set; } = null!;
}

public sealed class QrCodeController {
   [ControllerHandler("/api/qrcode", HttpMethodType.POST)]
   private async Task QrCodeHandler(Request request, Response response, CancellationToken cancellationToken) {
   }
}