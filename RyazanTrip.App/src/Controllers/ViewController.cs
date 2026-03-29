using RyazanTrip.App.Views;

using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Models;

namespace RyazanTrip.App.Controllers;

public sealed class ViewController : Controller {
   [ControllerHandler("/", HttpMethodType.GET)]
   private async Task MainPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new MainView(), request, response, cancellationToken);
   }

   [ControllerHandler("/authorization", HttpMethodType.GET)]
   private async Task
      AuthorizationPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      request.Session?.Set("test", [0]);
      await SendPage(new AuthorizationView(), request, response, cancellationToken);
   }

   [ControllerHandler("/admin", HttpMethodType.GET)]
   private async Task AdminPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      var userModel = await UserModel.LoadUserFromRequest(request.Session!, cancellationToken);
      if (userModel != null && userModel.CheckAdmin()) {
         await SendPage(new AdminView(), request, response, cancellationToken);
         return;
      }
      response.Redirect("/authorization");
      await response.SendAsync(cancellationToken);
   }

   [ControllerHandler("/profile", HttpMethodType.GET)]
   private async Task ProfilePageHandler(Request request, Response response, CancellationToken cancellationToken) {
      var userModel = await UserModel.LoadUserFromRequest(request.Session!, cancellationToken);
      if (userModel != null && userModel.Check()) {
         await SendPage(new ProfileView(), request, response, cancellationToken);
         return;
      }
      response.Redirect("/authorization");
      await response.SendAsync(cancellationToken);
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

   [ControllerHandler("/mushrooms", HttpMethodType.GET)]
   private async Task MushroomsPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new MushroomQuestView(), request, response, cancellationToken);
   }


   [ControllerHandler("/tours", HttpMethodType.GET)]
   private async Task ToursPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new TourView(), request, response, cancellationToken);
   }
   
   [ControllerHandler("/mushrooms_map", HttpMethodType.GET)]
   private async Task MushroomsMapPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new MushroomMapView(), request, response, cancellationToken);
   }
   
      
   [ControllerHandler("/rewards", HttpMethodType.GET)]
   private async Task RewardsPageHandler(Request request, Response response, CancellationToken cancellationToken) {
      await SendPage(new RewardsView(), request, response, cancellationToken);
   }



   private static async Task SendPage(View view, Request request, Response response,
      CancellationToken cancellationToken) {
      await view.Html(request, response, cancellationToken);
      response.WriteViewToCache(view);
   }
}