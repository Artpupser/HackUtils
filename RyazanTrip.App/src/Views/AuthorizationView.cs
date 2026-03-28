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
      await Start(request, response, string.Empty, cancellationToken);
      var sb = Builder;
      sb.Append("""
                <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100">
                       <div class="auth-card">
                           <div id="login-form" class="auth-form active">
                               <div class="auth-header">
                                   <h1 class="auth-title">ВХОД</h1>
                                   <button id="show_button_register" type="button" class="btn-toggle">регистрация</button>
                               </div>
                               <form id='login_form'>
                                   <div class="input-wrapper">
                                       <span class="input-icon"><i class="bi bi-person-fill"></i></span>
                                       <input name='email' id='email' type="email" class="form-control-auth" placeholder="почта" required>
                                   </div>

                                   <div class="input-wrapper">
                                       <span class="input-icon"><i class="bi bi-lock-fill"></i></span>
                                       <input name='password' type="password" class="form-control-auth" placeholder="пароль" required>
                                   </div>
                                   <button type="submit" class="btn-auth">войти</button>
                                   <div class="auth-options">
                                       <label class="remember-label">
                                           <input type="checkbox" class="custom-checkbox"> запомнить меня
                                       </label>
                                       <a href="#" class="forgot-link">забыли пароль?</a>
                                   </div>
                               </form>
                           </div>
                           <div id="register-form" class="auth-form" style="display: none;">
                               
                               <div class="auth-header">
                                   <h1 class="auth-title">РЕГИСТРАЦИЯ</h1>
                                   <button id='show_button_login' type="button" class="btn-toggle">вход</button>
                               </div>

                               <form id='registration_form'>
                                   <div class="input-wrapper">
                                       <span class="input-icon"><i class="bi bi-person-fill"></i></span>
                                       <input name='username' type="text" class="form-control-auth" placeholder="имя" required>
                                   </div>
                                   <div class="input-wrapper">
                                       <input name='email' type="email" class="form-control-auth" placeholder="email" required>
                                   </div>
                                   <div class="input-wrapper">
                                       <select name='town' class="form-control-auth" required>
                                           <option value="" disabled selected>Из какого вы города?</option>
                                           <option value="moscow">Москва</option>
                                           <option value="spb">Санкт-Петербург</option>
                                           <option value="ryazan">Рязань</option>
                                           <option value="other">Другой</option>
                                       </select>
                                       <span class="select-arrow"><i class="bi bi-chevron-down"></i></span>
                                   </div>
                                   <div class="input-wrapper">
                                       <span class="input-icon"><i class="bi bi-lock-fill"></i></span>
                                       <input name='password' type="password" class="form-control-auth" placeholder="пароль" required>
                                   </div>
                                   <div class="input-wrapper">
                                       <input name='password_repeat' type="password" class="form-control-auth" placeholder="повторите пароль" required>
                                   </div>
                                   <button type="submit" class="btn-auth">зарегистрироваться</button>
                                   <button id='yandex_submit' type="button" class="btn-auth">Зарегистрироваться с помощью яндекс</button>
                               </form>
                           </div>

                       </div>
                   </div>
                """);
      sb.Append(TagJs(response,"authorization.js"));
      await End(request, response, cancellationToken);
   }

   public override string Title => "Авторизация";
}