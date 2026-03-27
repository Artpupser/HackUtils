using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class AccountView : View {
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
                <div class="profile-header">
                    <div class="location-badge">
                        <i class="bi bi-geo-alt-fill"></i>
                        <span>Рязань</span>
                    </div>

                    <div class="user-info">
                        <div class="user-avatar">
                            <i class="bi bi-person-fill"></i>
                        </div>
                        
                        <div class="user-details">
                            <h1 class="user-name">Макарова Алиса Алексеевна</h1>
                            
                            <div class="stats-row">
                                <div class="stat-badge level">
                                    <span>Уровень:</span>
                                    <span class="level-number">1</span>
                                </div>
                                
                                <div class="stat-badge awards">
                                    <span>Мои Награды</span>
                                </div>
                            </div>
                            
                            <div class="xp-info">
                                <span>До следующего уровня:</span>
                                <span class="xp-needed">100 опыта</span>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="profile-content">
                    <div class="content-grid">
                        
                        <div class="menu-section">
                            
                            <button class="menu-item">
                                <div class="menu-header">
                                    <span class="menu-title">Личные Данные:</span>
                                    <i class="bi bi-chevron-double-right menu-arrow"></i>
                                </div>
                            </button>

                            <button class="menu-item">
                                <div class="menu-header">
                                    <span class="menu-title">Мои Туры:</span>
                                    <i class="bi bi-chevron-double-right menu-arrow"></i>
                                </div>
                                
                                <div class="tour-card">
                                    <img src="https://images.unsplash.com/photo-1564507592333-c60657eea523?w=200&h=150&fit=crop" 
                                         alt="Тур" 
                                         class="tour-image">
                                    <div class="tour-info">
                                        <div class="tour-date">23.06.2026</div>
                                    </div>
                                </div>
                                
                                <div class="tour-xp">После посещения тура - начислиться 150 опыта!</div>
                            </button>
                        </div>
                        <div class="rewards-card">
                            <p class="rewards-text">
                                Копи опыт и получай награды!<br><br>
                                Получай 50 очков опыта за каждое фото с нашими памятниками грибов!
                            </p>
                            
                            <a href="#" class="rewards-link">
                                <span>Подробнее</span>
                                <i class="bi bi-play-fill"></i>
                            </a>
                        </div>
                    </div>
                </div>
                """);
      await End(request, response, cancellationToken);
   }

   public override string Title => "Личный кабинет";
}