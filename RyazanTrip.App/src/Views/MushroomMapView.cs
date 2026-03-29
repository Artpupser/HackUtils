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
<link nonce='{{response.Nonce}}' rel="stylesheet" href="https://unpkg.com/leaflet/dist/leaflet.css"/>

<style>
   #map {width:100%; height:100%;}
.leaflet-control-attribution {
    display: none !important;
}
#infoBox{
 position:absolute;
 top:10px; left:10px;
 background:white;
 padding:10px 15px;
 border-radius:8px;
 z-index:1000;
 box-shadow:0 0 10px rgba(0,0,0,0.2);
 max-width:250px;
}

#startBtn{
 position:absolute;
 bottom:20px; left:50%;
 transform:translateX(-50%);
 z-index:2000;
 padding:14px 22px;
 border:none; border-radius:30px;
 background:#2ecc71; color:white; font-size:18px;
 cursor:pointer;
}

#scannerModal{
 position:fixed; top:0; left:0;
 width:100%; height:100%;
 background:rgba(0,0,0,0.6);
 display:none; justify-content:center; align-items:center;
 z-index:5000;
}

#scannerWindow{
 width:95%; max-width:420px;
 background:white; border-radius:20px; overflow:hidden;
 box-shadow:0 0 25px rgba(0,0,0,0.3);
 animation:pop 0.25s ease;
}

@keyframes pop{
 from{transform:scale(0.8);opacity:0} to{transform:scale(1);opacity:1}
}

#scannerHeader{
 display:flex; justify-content:space-between; align-items:center;
 padding:12px 16px; background:#2ecc71; color:white; font-weight:bold;
}

#closeScanner{background:none; border:none; color:white; font-size:20px; cursor:pointer;}

#finalModal{
  position: fixed;
  top:0; left:0;
  width:100%; height:100%;
  background: rgba(0,0,0,0.6);
  display:none; justify-content:center; align-items:center;
  z-index:6000;
}

#finalWindow{
  background:white;
  padding:30px 25px;
  border-radius:20px;
  text-align:center;
  box-shadow:0 0 25px rgba(0,0,0,0.4);
  animation:pop 0.3s ease;
}

#finalWindow h2{margin-bottom:15px; color:#2ecc71;}
#finalWindow p{margin-bottom:25px; font-size:16px;}
#finalWindow button{
  background:#ff7a00;
  color:white;
  border:none;
  padding:12px 25px;
  font-size:16px;
  border-radius:20px;
  cursor:pointer;
}
body, html {margin:0; height:100%; font-family:sans-serif;}
</style>

<script nonce='{{response.Nonce}}' src="https://unpkg.com/leaflet/dist/leaflet.js" defer></script>
<script nonce='{{response.Nonce}}' src="https://unpkg.com/html5-qrcode@2.3.8/html5-qrcode.min.js" defer></script>

""");

        sb.Append($"""
<div id="map"></div>

<div id="infoBox">Нажмите «Начать квест»</div>
<button id="startBtn">📍 Начать квест</button>

<div id="scannerModal">
  <div id="scannerWindow">
    <div id="scannerHeader">
      <span>Сканируйте QR гриба 🍄</span>
      <button id="closeScanner">✕</button>
    </div>
    <div id="scanner"></div>
  </div>
</div>

<div id="finalModal" style="display:none;">
  <h2>Поздравляем! 🎉</h2>
  <p id="finalXP">Вы прошли квест! Получено XP: 0</p>
  <button id="rewardBtn">За наградой!</button>
</div>
""");
        // ТВОЙ app.js подключаем ПОСЛЕДНИМ и с defer
        sb.Append($"""
                    <script nonce='{response.Nonce}' src="/api/public/files?name=app.js" defer></script>
                    """);

        await End(request, response, cancellationToken);
    }

    public override string Title => "Грибной квест";
}