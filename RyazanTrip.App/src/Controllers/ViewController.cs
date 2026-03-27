using RyazanTrip.App.Views;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Middleware;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Middleware;

namespace RyazanTrip.App.Controllers;

public sealed class ViewController : Controller {
   [ControllerHandler("/", HttpMethodType.GET)]
   private async Task MainPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new MainView(), request, response, cancellationToken);
   }

   [ControllerHandler("/authorization", HttpMethodType.GET)]
   private async Task
      AuthorizationPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new AuthorizationView(), request, response, cancellationToken);
   }

   [ControllerHandler("/admin", HttpMethodType.GET)]
   private async Task AdminPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new AdminView(), request, response, cancellationToken);
   }

   [ControllerHandler("/account", HttpMethodType.GET)]
   private async Task AccountPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new AccountView(), request, response, cancellationToken);
   }

   [ControllerHandler("/about_us", HttpMethodType.GET)]
   private async Task AboutUsPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new AboutUsView(), request, response, cancellationToken);
   }

   [ControllerHandler("/author_us", HttpMethodType.GET)]
   private async Task
      AboutUsAuthorPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new AboutAuthorsView(), request, response, cancellationToken);
   }

   [ControllerHandler("/achievement", HttpMethodType.GET)]
   private async Task AchievementPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new MushroomsView(), request, response, cancellationToken);
   }

   private static async Task SendPage(View view, Request request, Response response,
      CancellationToken cancellationToken) {
      await view.Html(request, response, cancellationToken);
      response.WriteViewToCache(view);
   }
}