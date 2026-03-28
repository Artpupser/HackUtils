using Microsoft.EntityFrameworkCore;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Middleware;
using PupaMVCF.Framework.Routing;

using RyazanTrip.App.Controllers;
using RyazanTrip.App.Middleware;
using RyazanTrip.App.Services;
using RyazanTrip.DataAccess.Postgres;

namespace RyazanTrip.App;

public static class Program {
   private static async Task Main(string[] args) {
      var builder = Host.CreateApplicationBuilder(args);
      builder.Services.AddDbContext<RyazanTripDbContext>(options => {
         options.UseNpgsql(builder.Configuration["DATABASE_CONNECTION_STRING"]);
      });

      builder.Configuration.AddEnvironmentVariables();
      builder.Services.AddSingleton<IRouter, Router>(_ => {
         var routerMapBuilder = new RouterMapBuilder();
         routerMapBuilder.AddMiddlewareRange([new LoggerMiddleware(), new TemplateMiddleware()]);
         routerMapBuilder.AddControllerRange([
            new StaticController(), new ViewController(), new ErrorController(), new AuthorizationController()
         ]);
         return new Router(routerMapBuilder);
      });
      builder.Services.AddSingleton<YandexMicroService>();
      builder.Services.AddHostedService<RyazanTripApp>();
      var host = builder.Build();
      const byte attemptsToConnect = 10;
      for (var i = 0; i < attemptsToConnect; i++) {
         
         using var scope = host.Services.CreateScope();
         var logger = scope.ServiceProvider.GetRequiredService<ILogger<PupaMVCF.Framework.Core.WebApp>>();
         try {
            logger.LogInformation("DB: {db}", builder.Configuration["DATABASE_CONNECTION_STRING"]);
            var db = scope.ServiceProvider.GetRequiredService<RyazanTripDbContext>();
            await db.Database.MigrateAsync();
         } catch (Exception e) {
            await Task.Delay(1000);
            logger.LogInformation("Restart...");
         }
      }
      await host.RunAsync();
   }
}