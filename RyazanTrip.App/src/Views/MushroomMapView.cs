using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MushroomMapView : View
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
        await Start(request, response, "mushroom-quest-page", cancellationToken);

        var sb = Builder;
        sb.Append($$"""
           <link rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css"/>
           <script nonce='{{response.Nonce}}' src="https://unpkg.com/leaflet/dist/leaflet.js"></script>
           <script nonce='{{response.Nonce}}' src="https://unpkg.com/html5-qrcode@2.3.8/html5-qrcode.min.js"></script>
           """);
        sb.Append($"""
        <!-- Leaflet -->
       

        <div id="map"></div>

        <div id="infoBox">Нажмите «Начать квест»</div>
        <button id="startBtn">📍 Начать квест</button>

        <!-- Модальное окно сканера -->
        <div id="scannerModal">
          <div id="scannerWindow">
            <div id="scannerHeader">
              <span>Сканируйте QR гриба 🍄</span>
              <button id="closeScanner">✕</button>
            </div>
            <div id="scanner"></div>
          </div>
        </div>

        <!-- Финальное окно -->
        <div id="finalModal" style="display:none;">
          <h2>Поздравляем! 🎉</h2>
          <p id="finalXP">Вы прошли квест! Получено XP: 0</p>
          <button id="rewardBtn">За наградой!</button>
        </div>
        """);
        sb.Append(TagJs(response,"app.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Грибной квест";
}