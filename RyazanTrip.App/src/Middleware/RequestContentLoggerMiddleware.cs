using Microsoft.Extensions.Logging;

using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Middleware;

namespace RyazanTrip.App.Middleware;

public sealed class RequestContentLoggerMiddleware : IMiddleware {
   public async Task<bool> Invoke(Request request, Response response, CancellationToken cancellationToken) {
      WebApp.SecureContextInstance.Logger.LogInformation("Content string: {Str}",
         await request.ReadContentStr(cancellationToken));
      return true;
   }
}