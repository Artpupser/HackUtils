using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class AboutAuthorsView : View {
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
      await Start(request, response,"body_about_authors", cancellationToken);
      var sb = Builder;
      sb.Append($"""
                 <section class="section_about_authors">
                        <h1 class="h1_about_authors">Об авторах:</h1>
                        Мы — команда программистов, которые <br> 
                        делают сайт Ryazan Trip своими <br>
                        руками. Без отделов, без долгих <br>
                        согласований — просто берем и делаем.<br>
                        <br>
                        Нам важно, чтобы сайт был удобным, <br>
                        живым и отражал ту самую атмосферу, <br>
                        которая ждет гостей на экскурсиях. Мы <br>
                        постоянно что-то улучшаем, тестируем, <br>
                        переделываем и радуемся, когда всё <br>
                        работает как надо.<br>
                        <br>
                        Мы сами живем в Рязани и относимся к <br>
                        проекту как к своему маленькому делу <br>
                        — честно, с душой и вниманием к <br>
                        деталям.
                    </section>
                    <img src="/api/public/files?name=bg_for_abo.webp" alt="image" class="bg_for_abo">
                 """);
      await End(request, response, cancellationToken);
   }

   public override string Title => "Об авторах";
}