using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Routing;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Services;
using RyazanTrip.DataAccess.Postgres;

namespace RyazanTrip.App;

public sealed class RyazanTripApp : WebApp, IRyazanTripAppSecureContext {
   public YandexMicroService YandexMicroMicroService { get; }
   public RyazanTripDbContext Context { get; }
   public static IRyazanTripAppSecureContext Instance { get; private set; } = null!;
   [ConfigurationKeyName("SelectelAccess")]
   public static ushort Port { get; private set; } = 50001;
   public RyazanTripApp(IConfiguration configuration, IRouter router, ILogger<RyazanTripApp> logger,
      RyazanTripDbContext dbContext, YandexMicroService yandexMicroService) : base(
      configuration, router,
      logger) {
      if (Instance != null)
         throw new InvalidOperationException("TripApp provider has already been configured");
      Context = dbContext;
      YandexMicroMicroService = yandexMicroService;
      Instance = this;
   }
}