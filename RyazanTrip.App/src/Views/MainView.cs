using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MainView : View
{
    #region START_END

    protected override async Task End(Request request, Response response, CancellationToken cancellationToken)
    {
        await base.End(request, response, cancellationToken);
        await new FooterComponent(this).Html(request, response, cancellationToken);
    }

    protected override async Task Start(Request request, Response response, string stylesForBody, CancellationToken cancellationToken)
    {
        await base.Start(request, response, stylesForBody, cancellationToken);
        await new HeaderComponent(this).Html(request, response, cancellationToken);
    }

    #endregion


    public override async Task Html(Request request, Response response, CancellationToken cancellationToken)
    {
        await Start(request,response,string.Empty,cancellationToken);
        var sb = Builder;
        sb.Append("""
                  <section id='hero' class='bg-black text-light d-flex align-items-center py-5 min-vh-100'>
                      <div class='container'>
                          <div class='row justify-content-center text-center'>
                              <div class='col-lg-8 col-xl-8 p-5'>
                                  <h1 class='fade show display-3 fw-bold mb-4'>Привет мир! 🌍</h1>
                                  <p class='lead fs-2'>Начало начал!</p>
                              </div>
                          </div>
                      </div>
                  </section>
                  """);
        sb.Append(TagJs(response,"index.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Главная страница";
}