using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;
using System.Text;

namespace RyazanTrip.App.Views;

public sealed class BonusesView : View
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
        await Start(request, response, "tour-app", cancellationToken);

        var sb = new StringBuilder();

        sb.Append("""
<!-- ШАПКА -->
<div class="tour-profile-header">
    <h2 class="tour-rewards-header-title">Ваши награды:</h2>
    <p class="tour-rewards-header-subtitle">Копи опыт - получай награды!</p>
</div>

<!-- ОСНОВНОЙ КОНТЕНТ НАГРАД -->
<div class="tour-rewards-page">
    <div class="container">
        
        <!-- Сетка наград -->
        <div class="tour-rewards-grid">
            
            <!-- Карточка 1 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

            <!-- Карточка 2 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

            <!-- Карточка 3 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

            <!-- Карточка 4 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

            <!-- Карточка 5 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

            <!-- Карточка 6 -->
            <div class="tour-reward-card">
                <div class="tour-reward-inner">
                    <i class="bi bi-gift-fill tour-reward-icon"></i>
                    <h3 class="tour-reward-title">Скидка 15% На<br>Сувениры</h3>
                    
                    <div class="tour-reward-progress">
                        <div class="tour-reward-progress-fill" style="width: 20%;"></div>
                    </div>
                    <div class="tour-reward-progress-text">
                        <span>Опыт:</span>
                        <span>20 / 100</span>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
""");

        await response.WriteAsync(sb.ToString(), cancellationToken);

        sb.Clear();
        sb.Append(TagJs(response, "bonuses.js"));
        sb.Append(TagJs(response, "bootstrap.js"));
        await response.WriteAsync(sb.ToString(), cancellationToken);

        await End(request, response, cancellationToken);
    }

    public override string Title => "Бонусы";
}