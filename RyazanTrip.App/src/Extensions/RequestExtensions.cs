using PupaMVCF.Framework.Core;

using RyazanTrip.App.Controllers;

namespace RyazanTrip.App.Extensions;

public static class RequestExtensions {
   public static async Task SendSuccess(this Response response, bool success, CancellationToken cancellationToken) {
      var successResponse = new SuccessResponse {
         Success = success
      };
      response.WriteTJsonToCache(successResponse);
      await response.SendAsync(cancellationToken);
   }
}