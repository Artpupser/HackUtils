using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Routing;

namespace RyazanTrip.App;

public sealed class RyazanTripApp : WebApp {
   public RyazanTripApp(IConfiguration configuration, IRouter router, ILogger<RyazanTripApp> logger) : base(
      configuration, router,
      logger) { }
}