using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class RewardsView : View
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
        await Start(request, response, "tour-app", cancellationToken);

        var sb = Builder;

        sb.Append("""

                  <div class='tour-profile-header'>
                      <h2 class='tour-rewards-header-title'>Ваши награды:</h2>
                      <p class='tour-rewards-header-subtitle'>Копи опыт - получай награды!</p>
                  </div>

                  <div class='tour-rewards-page'>
                      <div class='container'>
                          <div class='tour-rewards-grid'>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 15% На<br>Сувениры</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 5% На<br>Рестораны</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 15% На<br>Такси</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 50% На<br>Транспорт</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 20% На<br>Музеи</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                              <div class='tour-reward-card'>
                                  <div class='tour-reward-inner'>
                                      <i class='bi bi-gift-fill tour-reward-icon'></i>
                                      <h3 class='tour-reward-title'>Скидка 15% На<br>Экскурсии</h3>
                                      <div class='tour-reward-progress'>
                                          <div class='tour-reward-progress-fill' style='width: --tour-reward-progress-fill-var;'></div>
                                      </div>
                                      <div class='tour-reward-progress-text'>
                                          <span>Опыт:</span><p class='progressText'>0%</p>
                                      </div>
                                  </div>
                              </div>

                          </div>
                      </div>
                  </div>

                  """);
        await End(request, response, cancellationToken);
    }

    public override string Title => "Ваши награды";
}