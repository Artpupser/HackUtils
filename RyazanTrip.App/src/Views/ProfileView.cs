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
      await Start(request, response, "tour-app", cancellationToken);

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
          <div class="tour-profile-header">
              <div class="tour-location-badge">
                  <i class="bi bi-geo-alt-fill"></i>
                  <span>{{town}}</span>
              </div>

              <div class="tour-user-info">
                  <div class="tour-user-avatar">
                      <i class="bi bi-person-fill"></i>
                  </div>
                  
                  <div class="tour-user-details">
                      <h1 class="tour-user-name">{{userModel.UserEntity.Username}}</h1>
                      
                      <div class="tour-stats-row">
                          <div class="tour-stat-badge level">
                              <span>Уровень:</span>
                              <span class="tour-level-number">{{userModel.UserEntity.LevelEntity?.Name ?? "Неизвестно"}}</span>
                          </div>
                          
                          <div class="tour-stat-badge awards">
                              <span>Мои Награды</span>
                          </div>
                      </div>
                      
                      <div class="tour-xp-info">
                          <span>До следующего уровня:</span>
                          <span class="tour-xp-needed">{{nextLevelXp}} опыта</span>
                      </div>
                  </div>
              </div>
          </div>
       <div class="tour-profile-content">
           <div class="tour-content-grid">
               <div class="tour-menu-section">
                   <button class="tour-menu-item">
                       <div class="tour-menu-header">
                           <span class="tour-menu-title">Личные Данные:</span>
                           <i class="bi bi-chevron-double-right tour-menu-arrow"></i>
                       </div>
                   </button>
                   <button class="tour-menu-item">
                       <div class="tour-menu-header">
                           <span class="tour-menu-title">Мои Туры:</span>
                           <i class="bi bi-chevron-double-right tour-menu-arrow"></i>
                       </div>
   """);
            if (userModel.UserEntity.UserTours.Any()) {
             foreach (var userTour in userModel.UserEntity.UserTours) {
                 var imageUrl = userTour.TourEntity?.ImageEntity?.Url ?? "/api/public/files?name=default_tour.webp";
                 sb.Append($"""
                             <div class="tour-tour-card">
                                 <img src="{imageUrl}" 
                                      alt="Тур" 
                                      class="tour-tour-image">
                                 <div class="tour-tour-info">
                                     <div class="tour-tour-date">{userTour.TourEntity?.TourTime.ToString("dd.MM.yyyy HH:mm") ?? "–"}</div>
                                 </div>
                             </div>
                 """);
             }
         } else {
             sb.Append("""
                 <div class="empty-tours">У вас пока нет посещённых туров</div>
             """);
         }

         sb.Append($$"""
                             <div class="tour-tour-xp">После посещения тура - начислится 150 опыта!</div>
                         </button>
                     </div>

                     <div class="tour-rewards-card">
                         <p class="tour-rewards-text">
                             Копи опыт и получай награды!<br><br>
                             Получай 50 очков опыта за каждое фото с нашими памятниками грибов!
                         </p>
                         
                         <a href="#" class="tour-rewards-link">
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