using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public class TourView : View {
   
   #region START_END

   protected override async Task Start(Request request, Response response, string stylesForBody, CancellationToken cancellationToken)
   {
      await base.Start(request, response, stylesForBody, cancellationToken);
      await new HeaderComponent(this).Html(request, response, cancellationToken);
   }

   protected override async Task End(Request request, Response response, CancellationToken cancellationToken)
   {
      await base.End(request, response, cancellationToken);
      await new FooterComponent(this).Html(request, response, cancellationToken);
   }

   #endregion
   
   public override async Task Html(Request request, Response response, CancellationToken cancellationToken) {
      await Start(request, response, "tour-app", cancellationToken);
      var sb = Builder;
      sb.Append("""
                <div class="tour-catalog-container">
                       <div class="tour-catalog-header">
                           <h1 class="tour-catalog-title">КАТАЛОГ ТУРА</h1>
                       </div>
                       <div id="toursContainer" class="tour-catalog-list">
                       </div>
                
                       <div id="emptyState" class="tour-empty-state" style="display: none;">
                           <i class="bi bi-inbox"></i>
                           <p>Туры пока не добавлены</p>
                       </div>
                   </div>
                """);
      sb.Append(TagJs(response,"catalog.js"));
      await End(request, response, cancellationToken);
   }

   public override string Title => "Туры";
}