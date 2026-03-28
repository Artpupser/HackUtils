using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class AboutUsView : View {
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
      await Start(request, response,"about-page", cancellationToken);
      var sb = Builder;
      sb.Append($"""
                 <div class="about-section">
                   <h1 class="about-title">О нашем стартапе:</h1>
                   <div class="about-text">
                     Ryazan Trip родился из любви к городу и желания показать его таким, каким мы сами<br>
                     его ценим — живым, искренним, непарадным.<br><br>
                     Мы не работаем по сценарию. Каждая прогулка — это диалог, где гостю одинаково интересны и<br>
                     история улиц, и случайная встреча, и неожиданный вид с неочевидной точки.<br><br>
                     Нам важно, чтобы вы не просто узнали факты, а почувствовали ритм Рязани, нашли в ней<br>
                     что-то своё и, возможно, захотели вернуться.<br><br>
                     Никакой суеты. Только вы, город и те, кто знает его лучше всего.
                   </div>
                   <img src="imgs/backgrounds/bg_for_abo.webp" alt="Рязань" class="about-img">
                 </div>
                 
                 """);
      await End(request, response, cancellationToken);
   }

   public override string Title => "О нас";
}