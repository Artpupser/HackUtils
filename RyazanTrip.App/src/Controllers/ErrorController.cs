using PupaMVCF.Framework.Controllers;
using PupaMVCF.Framework.Core;
using RyazanTrip.App.Views;

namespace RyazanTrip.App.Controllers;

public sealed class ErrorController : BasicErrorController {
   protected override async Task ErrorHtmlHandler(Request request, Response response,
      CancellationToken cancellationToken) {
      var errorView = new ErrorView(response);
      await errorView.Html(request, response, cancellationToken);
      response.WriteViewToCache(errorView);
   }
}