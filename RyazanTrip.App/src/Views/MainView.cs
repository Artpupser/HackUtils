using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MainView : View {
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
                <img src="/api/public/files?name=background.webp" alt="" class="back_main">
                <section class="hero py-5">
                  <div class="container">
                """);
         sb.Append("""
                    <h1 class="mb-4 fw-bold">БЛИЖАЙШИЙ ТУР:</h1>
                    <div class="row align-items-center">
                      <div class="col-lg-6 mb-4 mb-lg-0">
                        <div class="hero-img">
                          <img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/cater_tour.webp" class="img-fluid rounded" alt="Катер">
                        </div>
                      </div>
                   """);
            sb.Append("""
                      <div class="col-lg-6">
                        <h2>Обзорная экскурсия<br>на катере</h2>
                        <p class="date fw-bold">Дата: 5 Июля</p>
                        <a href='/tours' class="but_tour fw-bold btn btn-outline-dark rounded-pill px-4">Узнать больше!</a>
                      </div>
                    </div>
                  </div>
                </section>
                <section class="about py-5">
                  <div class="container">
                    <div class="row">
                      <div class="col-lg-7 mb-4">
                        <h3>Коротко О Нас:</h3>
                        <p>
                          Добро пожаловать в Ryazan Trip — сообщество<br> для путешественников,
                          влюбленных в Рязань.
                        </p>
                        <p>
                          Мы создаем увлекательные маршруты по городу и <br> постоянно развиваемся,
                          чтобы ваши впечатления <br>были яркими и живыми.
                        </p>
                      </div>
                
                      <!-- Right -->
                      <div class="col-lg-5">
                        <div class="philosophy p-4 rounded-30">
                          <h2>Наша философия:</h2>
                          <p>
                            Мы не просто показываем город, а влюбляем в него через искренность,
                            живое общение и настоящую атмосферу Рязани.
                          </p>
                        </div>
                      </div>
                    </div>
                
                    <!-- Features -->
                    <div class="text_nums row text-left mt-5">
                      
                      <div class="col-md-3 mb-4">
                        <div class="circle fs-1">1</div>
                        <h2>Гиды:</h2>
                        <p>Вы в надежных руках местных экспертов</p>
                      </div>
                
                      <div class="col-md-3 mb-4">
                        <div class="circle fs-1">2</div>
                        <h2>Маршруты:</h2>
                        <p>Показываем Рязань так, как её не знают туристы</p>
                      </div>
                
                      <div class="col-md-3 mb-4">
                        <div class="circle fs-1">3</div>
                        <h2>Знакомства:</h2>
                        <p>Открывайте людей, места и истории вместе с нами</p>
                      </div>
                
                      <div class="col-md-3 mb-4">
                        <div class="circle fs-1">4</div>
                        <h2>Впечатления:</h2>
                        <p>Яркие моменты, которые останутся с вами после путешествия</p>
                      </div>
                    </div>
                  </div>
                </section>
                <section class="tours_slider">
                  <div class="container">
                  <h1 class="mb-4 fw-bold">Туры:</h1>
                  </div>
                
                
                  <div class="slider slider-container">
                
                  <button class="btn_slide prev fs-1"><img src="/api/public/files?name=caret-left-fill.webp"></button>
                
                  <div class="slider-window">
                    <div class="slides">
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/cater_tour.webp"></div>
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/fish_village_tour.webp"></div>
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/old_ryazan_tour.webp"></div>
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/old_town_hotel_tour.webp"></div>
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/pochtovaya_street_tour.webp"></div>
                      <div class="slide"><img src="https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/rzn_kremlin_tour.webp"></div>
                    </div>
                  </div>
                  <button class="btn_slide next fs-1"><img src="/api/public/files?name=caret-right-fill.webp"></button>
                </div><img src="/api/public/files?name=bg_for_main.webp" alt="oldtown" class="bg_for_main"></section>
                """);
      sb.Append(TagJs(response,"script.js"));
      await End(request, response, cancellationToken);
   }

   public override string Title => "Главная страница";
}
