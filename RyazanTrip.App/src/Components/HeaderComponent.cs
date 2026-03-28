using PupaMVCF.Framework.Components;
using PupaMVCF.Framework.Core;

namespace RyazanTrip.App.Components;

public record NavbarLink(string Name, string Href);

public sealed class HeaderComponent : Component {
   private static readonly NavbarLink[] NavbarLinks = [
      new("Туры", "/tours"),
      new("О нас", "/about_us"),
      // new("Связаться с нами", "/contact_us")
   ];

   public HeaderComponent(IComponentParent? parent) : base(parent) { }

   public override Task Html(Request request, Response response, CancellationToken cancellationToken) {
        var sb = Builder;
        sb.Append("<header class=\"py-3\" style=\"background-color: #8D8741;\"><div class=\"container d-flex align-items-center justify-content-between\"><a href='/' class=\"d-flex align-items-center gap-3\"><img src=\"/api/public/files?name=logo.webp\" alt=\"logo\" class=\"rounded-circle\" style=\"width:80px; height:80px; background-color:white;\"><span class=\"fw-normal\" style=\"font-size:40px;\">Ryazan Trip</span></a><nav class=\"d-none d-md-flex gap-5\">");
        foreach (var navLink in NavbarLinks)
            sb.Append($"<a href=\"{navLink.Href}\" class=\"text-decoration-none\" style=\"font-size:22px; transition:0.3s;\">{navLink.Name}</a>");
        sb.Append("</nav><div class=\"d-md-none\"><button class=\"btn btn-burger\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#mobileMenu\">☰</a></div><div class=\"d-flex justify-content-end\"><div class=\"d-flex align-items-center\"><a href=\"/profile\"><img src=\"/api/public/files?name=person-circle.webp\" alt=\"profile\" class=\"rounded-circle\" style=\"width:80px; height:80px; background-color:#8D8741;\"></a></div></div></div><div class=\"collapse\" id=\"mobileMenu\"><div class=\"p-3 d-flex flex-column gap-2\">");
        foreach (var navLink in NavbarLinks)
            sb.Append($"<a href=\"{navLink.Href}\" class=\"text-decoration-none\" style=\"font-size:22px; transition:0.3s;\">{navLink.Name}</a>");
        sb.Append("</div></div></header>");
        return Task.CompletedTask;
   }
}