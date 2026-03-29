using PupaMVCF.Framework.Components;
using PupaMVCF.Framework.Core;

namespace RyazanTrip.App.Components;

public sealed class FooterComponent : Component {
   public FooterComponent(IComponentParent? parent) : base(parent) { }
   private static readonly NavbarLink[] NavbarLinks = [
      new("О нашем стартапе", "/about_us"),
      new("Об авторах", "/author_us"),
   ];
   public override Task Html(Request request, Response response, CancellationToken cancellationToken) {
      var sb = Builder;
      sb.Append($"""
                 <footer class="pt-4 pb-3"><div class="container"><div class="row"><div class="col-md-3 mb-3"><h5 class="fw-bold mb-3">Больше Информации</h5><ul class="list-unstyled"> 
                 """);
      foreach (var navLink in NavbarLinks)
         sb.Append($"<li class=\"mb-1\"><a href=\"{navLink.Href}\" class=\"text-decoration-none\">{navLink.Name}</a></li>");
      sb.Append($"""
                 </ul></div><div class="col-md-4 mb-3"><h5 class="fw-bold mb-3">Контакты</h5><p class="mb-1"><span class="me-2"><img src="/api/public/files?name=telephone-plus-fill.webp" alt="phone" class="phone"></span>+7 999 760 27 65</p><p class="mb-1"><span class="me-2"><img src="/api/public/files?name=geo-alt-fill.webp" alt="geo" class="geo"></span>г. Рязань, Ул. Щадрина 43</p><p class="mb-0"><span class="me-2"><img src="/api/public/files?name=clock-fill.webp" alt="clock" class="clock"></span>Часы Работы: 9:00–21:00</p></div><div class="col-md-5 mb-3 d-flex flex-column flex-md-row justify-content-md-between align-items-md-center"><div class="mb-3 mb-md-0"><h5 class="fw-bold mb-3">Мы В Соц.Сетях!</h5><a href="#"><img src="/api/public/files?name=telegram.webp" alt="telegram" class="telegram"></a></div><div class="text-md-end"><img src="/api/public/files?name=logo.webp" alt="logo" class="rounded-circle" style="max-width: 120px;; background-color:white;"></div></div></div><div class="footer-divider my-3"></div><div class="row"><div class="col text-center small">
                 """);
      sb.Append($"""© {DateTime.Now.Year} Рязань Трип Rights Reserved.</div> </div></div></footer>""");
      return Task.CompletedTask;
   }
}