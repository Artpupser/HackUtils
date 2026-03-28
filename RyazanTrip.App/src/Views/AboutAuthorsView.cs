using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class AboutAuthorsView : View
{
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

    public override async Task Html(Request request, Response response, CancellationToken cancellationToken)
    {
        await Start(request, response, "about-page", cancellationToken);
        var sb = Builder;
        sb.Append(@"
          <div class='about-section'>
               <h1 class='about-title'>Об авторах:</h1>
                <div class='about-text'>
                  Мы — команда программистов, которые делают сайт Ryazan Trip своими руками.<br>
                  Без отделов, без долгих согласований — просто берем и делаем.<br><br>
                  Нам важно, чтобы сайт был удобным, живым и отражал ту самую атмосферу,<br>
                  которая ждет гостей на экскурсиях. Мы постоянно что-то улучшаем, тестируем,<br>
                  переделываем и радуемся, когда всё работает как надо.<br><br>
                  Мы сами живем в Рязани и относимся к проекту как к своему маленькому делу<br>
                  — честно, с душой и вниманием к деталям.
                </div>
               <img src='/api/public/files?name=bg_for_abo.webp' alt='Рязань' class='about-img'>
         </div>
      ");
        await End(request, response, cancellationToken);
    }

    public override string Title => "Об авторах";
}