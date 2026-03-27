using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MainView : View {
   #region START_END

   protected override async Task End(Request request, Response response, CancellationToken cancellationToken) {
      await base.End(request, response, cancellationToken);
      await new FooterComponent(this).Html(request, response, cancellationToken);
   }

   protected override async Task Start(Request request, Response response, string stylesForBody,
      CancellationToken cancellationToken) {
      await base.Start(request, response, stylesForBody, cancellationToken);
      await new HeaderComponent(this).Html(request, response, cancellationToken);
   }

   #endregion


   public override async Task Html(Request request, Response response, CancellationToken cancellationToken) {
      await Start(request, response, string.Empty, cancellationToken);
      var sb = Builder;
      sb.Append("""
                
                """);
      sb.Append(TagJs(response, "index.js"));
      await End(request, response, cancellationToken);
   }

   public override string Title => "Главная страница";
}