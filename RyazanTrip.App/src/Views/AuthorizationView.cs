using Grpc.Core;

using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class AuthorizationView : View {
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
      var sb = Builder;
      sb.Append("""
                <div class="tour-auth-container">
                       <div class="tour-auth-card">
                           <div id="login-form" class="tour-auth-form active">
                               <div class="tour-auth-header">
                                   <h1 class="tour-auth-title">ВХОД</h1>
                                   <button id='show_button_register' type="button" class="tour-btn-toggle" onclick="showRegisterForm()">регистрация</button>
                               </div>
                
                               <form>
                                   <div class="tour-input-wrapper">
                                       <span class="tour-input-icon"><i class="bi bi-person-fill"></i></span>
                                       <input type="email" class="tour-form-control" placeholder="почта" required>
                                   </div>
                
                                   <div class="tour-input-wrapper">
                                       <span class="tour-input-icon"><i class="bi bi-lock-fill"></i></span>
                                       <input type="password" class="tour-form-control" placeholder="пароль" required>
                                   </div>
                
                                   <button type="submit" class="tour-btn-auth">войти</button>
                
                                   <div class="tour-auth-options">
                                       <label class="tour-remember-label">
                                           <input type="checkbox" class="tour-custom-checkbox"> запомнить меня
                                       </label>
                                       <a href="recovery.html" class="tour-forgot-link">забыли пароль?</a>
                                   </div>
                               </form>
                           </div>
                <div id="register-form" class="tour-auth-form" style="display: none;">
                    <div class="tour-auth-header">
                        <h1 class="tour-auth-title">РЕГИСТРАЦИЯ</h1>
                        <button id='show_button_login' type="button" class="tour-btn-toggle" onclick="showLoginForm()">вход</button>
                    </div>
                
                    <form>
                        <div class="tour-input-wrapper">
                            <span class="tour-input-icon"><i class="bi bi-person-fill"></i></span>
                            <input type="text" class="tour-form-control" placeholder="имя" required>
                        </div>
                
                        <div class="tour-input-wrapper">
                            <input type="email" class="tour-form-control" placeholder="email" required>
                        </div>
                
                        <div class="tour-input-wrapper">
                            <select class="tour-form-control" required>
                                <option value="" disabled selected>Из какого вы города?</option>
                                <option value="moscow">Москва</option>
                                <option value="spb">Санкт-Петербург</option>
                                <option value="ryazan">Рязань</option>
                                <option value="other">Другой</option>
                            </select>
                            <span class="tour-select-arrow"><i class="bi bi-chevron-down"></i></span>
                        </div>
                
                        <div class="tour-input-wrapper">
                            <span class="tour-input-icon"><i class="bi bi-lock-fill"></i></span>
                            <input type="password" class="tour-form-control" placeholder="пароль" required>
                        </div>
                
                        <div class="tour-input-wrapper">
                            <input type="password" class="tour-form-control" placeholder="повторите пароль" required>
                        </div>
                
                        <button type="submit" class="tour-btn-auth">зарегистрироваться</button>
                """);
      await RyazanTripApp.Instance.YandexMicroMicroService.Connect(cancellationToken);
      var url = await RyazanTripApp.Instance.YandexMicroMicroService.GetAuthUrlAsync(cancellationToken);
      sb.Append(
         $"<a href='{url}' id='yandex_submit'class='tour-btn-auth tour-link-yandex'>Зарегистрироваться с помощью яндекс</a>");
      sb.Append("</form></div> </div></div>");
      sb.Append(TagJs(response,"authorization.js"));
      await End(request, response, cancellationToken);
   }

   public override string Title => "Авторизация";
}