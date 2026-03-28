using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;

using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MushroomsView : View {
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
  await Start(request, response, tour-app, cancellationToken);   
  public override async Task Html(
    Request request, 
    Response response, 
    CancellationToken cancellationToken)
{
    var sb = new System.Text.StringBuilder();

    sb.Append(@"
    <div class='tour-mushroom-page'>
        
        <!-- ШАПКА -->
        <div class='tour-mushroom-header'>
            <h1 class='tour-mushroom-title'>Грибной квест</h1>
            <p class='tour-mushroom-subtitle'>
                Собирай грибы — получай бонусы!<br>
                Гуляя по разным уголкам нашего города найди памятники-грибы. 
                Сделай фото с ними и загружай в личный кабинет. 
                <strong>50 баллов за каждый найденный гриб!</strong>
            </p>
        </div>

        <!-- СЕТКА ГРИБОВ -->
        <div class='tour-mushroom-grid'>
            
            <!-- Гриб 1 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_коробейник.webp' 
                         alt='Рязанский коробейник'>
                </div>
                <h3 class='tour-mushroom-name'>Рязанский коробейник</h3>
                <p class='tour-mushroom-location'>
                    Зазывает горожан неподолеку от торговых рядов на площади ленина
            </div>

            <!-- Гриб 2 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гри_мужичок_боровичок.webp' 
                         alt='Мужичок-боровичок'>
                </div>
                <h3 class='tour-mushroom-name'>Мужичок-боровичок</h3>
                <p class='tour-mushroom-location'>
                    расположился на главной пешеходной улице рязани - почтовой
                </p>
            </div>

            <!-- Гриб 3 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/грибная_пара.webp' 
                         alt='Влюбленная пара'>
                </div>
                <h3 class='tour-mushroom-name'>Влюбленная пара</h3>
                <p class='tour-mushroom-location'>
                    Прячится под зонтиком от капель фонтана на площади театральной
                </p>
            </div>

            <!-- Гриб 4 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/грибная_капелла.webp' 
                         alt='Грибная капелла'>
                </div>
                <h3 class='tour-mushroom-name'>Грибная капелла</h3>
                <p class='tour-mushroom-location'>
                    поющая команда расположилась на площади победы
                </p>
            </div>

            <!-- Гриб 5 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/грибная_команда.webp' 
                         alt='Грибная Команда'>
                </div>
                <h3 class='tour-mushroom-name'>Грибная команда</h3>
                <p class='tour-mushroom-location'>
                    По Лыбедскому бульвару озорные лисички катаются с горки на скейте.
                </p>
            </div>

            <!-- Гриб 6 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_путешственник.webp' 
                         alt='Гриб Путешественник'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб путешественник</h3>
                <p class='tour-mushroom-location'>
                    с рюкзаком за плечами увлеченно изучает карту на первомайском проспекте 
                </p>
            </div>

            <!-- Гриб 7 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_профессор.webp' 
                         alt='Гриб Профессор'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб профессор</h3>
                <p class='tour-mushroom-location'>
                    читает лекцию своим студентам. Скульптура расположилась под окнами Рязанского Политеха на улице Ленина, 53.
                </p>
            </div>

            <!-- Гриб 8 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_дозорный.webp' 
                         alt='Гриб дозорный'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб дозорный</h3>
                <p class='tour-mushroom-location'>
                    Пост «Гриба-дозорного» находится около Глебовского моста при входе в Кремль
                </p>
            </div>

            <!-- Гриб 9 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/Гриб_художник.webp' 
                         alt='Гриб художник'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб художник</h3>
                <p class='tour-mushroom-location'>
                    Неподалеку от дозороного на набережной Кремля разместился «Гриб-художник», рисующий рязанский пейзаж.
                </p>
            </div>

            <!-- Гриб 10 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_рыбак.webp' 
                         alt='Гриб Рыбак'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб Рыбак</h3>
                <p class='tour-mushroom-location'>
                    Самым целеустремленным собирателям стоит отправиться на берег пруда Родная заводь, неподалеку от школы №69 в микрорайоне Канищево
                </p>
            </div>

            <!-- Гриб 11 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_мудрец.webp' 
                         alt='Гриб Мудрец'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб мудрец</h3>
                <p class='tour-mushroom-location'>
                    находится справа от входа в Центральную городскую библиотеку имени Сергея Есенина (Первомайский просп., 74к1)
                </p>
            </div>

            <!-- Гриб 12 -->
            <div class='tour-mushroom-card'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/гриб_пионер.webp' 
                         alt='Гриб пионер'>
                </div>
                <h3 class='tour-mushroom-name'>Гриб Пионер</h3>
                <p class='tour-mushroom-location'>
                    Вспомнить свои детские мечты предлагает «Гриб-пионер», спрятавшийся в парке Дворца детского творчества
                </p>
            </div>

            <!-- Гриб 13 -->
            <div class='tour-mushroom-card full-width'>
                <div class='tour-mushroom-photo'>
                    <img src='imgs/mushrooms/грибы_с_глазами.webp' 
                         alt='Семейство «Грибов с глазами»'>
                </div>
                <h3 class='tour-mushroom-name'>Семейство «Грибов с глазами»</h3>
                <p class='tour-mushroom-location'>
                     — бородатый гриб-отец и его два сынишки — отдыхает в окружении прочей лесной живности в Нижнем городском саду
                </p>
            </div>

        </div>

    </div>

    <!-- Модальное окно загрузки фото -->
    <div class='modal fade' id='uploadModal' tabindex='-1' aria-hidden='true'>
        <div class='modal-dialog modal-dialog-centered'>
            <div class='modal-content tour-modal-content'>
                <div class='modal-header border-0'>
                    <h5 class='modal-title tour-modal-title'>Загрузить фото с грибом</h5>
                    <button type='button' class='btn-close' data-bs-dismiss='modal' aria-label='Закрыть'></button>
                </div>
                <div class='modal-body text-center'>
                    <div class='tour-upload-area' id='dropZone'>
                        <i class='bi bi-cloud-arrow-up tour-upload-icon'></i>
                        <p class='tour-upload-text'>Перетащите фото сюда или нажмите для выбора</p>
                        <input type='file' id='fileInput' accept='image/*' class='d-none'>
                        <button class='tour-btn-select' onclick='document.getElementById('fileInput').click()'>
                            Выбрать файл
                        </button>
                    </div>
                    <div id='previewContainer' class='d-none mt-3'>
                        <img id='imagePreview' class='tour-preview-img' alt='Превью'>
                        <button class='tour-btn-submit mt-3' onclick='submitPhoto()'>
                            Отправить фото
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

");

    sb.Append(TagJs(response, "mushroom.js"));
    sb.Append(TagJs(response, "bootstrap.js"));
  await End(request, response, cancellationToken);}

   public override string Title => "Собирательство";
}