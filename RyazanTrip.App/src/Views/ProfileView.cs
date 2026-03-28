using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;
using RyazanTrip.App.Models;

namespace RyazanTrip.App.Views;

public sealed class ProfileView : View {
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

      try
      {
         var sb = Builder;
         var userModel = await UserModel.IncludeUserFromRequest(request.Session!, cancellationToken);
         if (userModel == null) {
            WebApp.SecureContextInstance.Logger.LogInformation("User is NULL!");
            response.ErrorStack.PushStack("User undefined");
            return;
         }

         WebApp.SecureContextInstance.Logger.LogInformation("User loaded: {Username}, LevelId: {LevelId}", 
            userModel.UserEntity.Username, userModel.UserEntity.LevelId);

         // Load next level only if not at max level (5)
         var nextLevelXp = 0;
         if (userModel.UserEntity.LevelId < 5) {
            var nextLevel = await LevelModel.LoadFromId(userModel.UserEntity.LevelId + 1, cancellationToken);
            nextLevelXp = nextLevel?.LevelEntity?.RequiredExp ?? 0;
         } else {
            nextLevelXp = userModel.UserEntity.LevelEntity?.RequiredExp ?? 0;
         }

         var town = userModel.UserEntity.Town ?? "Рязань";
         
         sb.Append($$"""
                     <div class="profile-header">
                     <div class="location-badge">
                     <i class="bi bi-geo-alt-fill"></i>
                     <span>{{town}}</span>
                     </div>

                     <div class="user-info">
                     <div class="user-avatar">
                     <i class="bi bi-person-fill"></i>
                     </div>

                     <div class="user-details">
                     <h1 class="user-name">{{userModel.UserEntity.Username}}</h1>

                     <div class="stats-row">
                     <div class="stat-badge level">
                         <span>Уровень:</span>
                         <span class="level-number">{{userModel.UserEntity.LevelEntity?.Name ?? "Неизвестно"}}</span>
                     </div>

                     <div class="stat-badge awards">
                         <span>Мои Награды</span>
                     </div>
                     </div>

                     <div class="xp-info">
                     <span>До следующего уровня:</span>
                     <span class="xp-needed">{{nextLevelXp}} опыта</span>
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
                         <i class="bi bi-chevron-double-right menu-arrow"></i></div> 
                     """);

         if (userModel.UserEntity.UserTours.Any()) {
            foreach (var userTour in userModel.UserEntity.UserTours) {
               var imageUrl = userTour.TourEntity?.ImageEntity?.Url ?? "/api/public/files?name=default-tour.webp";
               sb.Append($"""
                           <div class="tour-card">
                               <img src="{imageUrl}" 
                                    alt="Тур" 
                                    class="tour-image">
                               <div class="tour-info">
                                   <div class="tour-date">{userTour.TourEntity?.TourTime}</div>
                                   <div class="tour-price">{userTour.TourEntity?.Price}</div>
                                   <div class="tour-description">{userTour.TourEntity?.Description}</div>
                               </div>
                           </div>
                         """);
            }
         } else {
            sb.Append("<div class=\"empty-tours\">У вас пока нет посещённых туров</div>");
         }

         sb.Append("""
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
      }
      catch (Exception e)
      {
         WebApp.SecureContextInstance.Logger.LogError("Error in profile view, {Message}", e);
         response.ErrorStack.PushStack($"Error loading profile: {e.Message}");
      }
      await End(request, response, cancellationToken);
   }

   public override string Title => "Личный кабинет";
}