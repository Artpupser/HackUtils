using System.Text.Json;
using System.Text.Json.Serialization;

using PupaMVCF.Framework.Core;

namespace RyazanTrip.App.Procedures;

public record LocationResponse() {
   [JsonPropertyName("city")] public string City { get; set; } = string.Empty;
   [JsonPropertyName("region")] public string Region { get; set; } = string.Empty;
   [JsonPropertyName("country_name")] public string CountryName { get; set; } = string.Empty;
}

public sealed class LocationProcedure : ProcedureBase {
   private readonly string _link;
   public LocationProcedure(Request request, string format = "json", string lang = "ru") : base(request) {
      _link = $"https://ipapi.co/{request.IpAddress}/json/";
   }
   
   public async Task<LocationResponse?> GetLocationAsync(CancellationToken cancellationToken) {
      var json = await HttpClient.GetStringAsync(_link, cancellationToken);
      return JsonSerializer.Deserialize<LocationResponse>(json);
   }
}