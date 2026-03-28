using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;
using System.Text;

namespace RyazanTrip.App.Views;

public sealed class MushroomsView : View
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
        await Start(request, response, "tour-app", cancellationToken);

        var sb = new StringBuilder();

        // Данные грибов (можно из БД)
        var mushrooms = new[]
        {
            new { Image = "гриб_коробейник.webp", Name = "Рязанский коробейник", Location = "Зазывает горожан неподолеку от торговых рядов на площади ленина" },
            new { Image = "гри_мужичок_боровичок.webp", Name = "Мужичок-боровичок", Location = "расположился на главной пешеходной улице рязани - почтовой" },
            new { Image = "грибная_пара.webp", Name = "Влюбленная пара", Location = "Прячется под зонтиком от капель фонтана на площади театральной" },
            new { Image = "грибная_капелла.webp", Name = "Грибная капелла", Location = "поющая команда расположилась на площади победы" },
            new { Image = "грибная_команда.webp", Name = "Грибная команда", Location = "По Лыбедскому бульвару озорные лисички катаются с горки на скейте." },
            new { Image = "гриб_путешственник.webp", Name = "Гриб путешественник", Location = "с рюкзаком за плечами увлеченно изучает карту на первомайском проспекте" },
            new { Image = "гриб_профессор.webp", Name = "Гриб профессор", Location = "читает лекцию своим студентам. Скульптура расположилась под окнами Рязанского Политеха на улице Ленина, 53." },
            new { Image = "гриб_дозорный.webp", Name = "Гриб дозорный", Location = "Пост «Гриба-дозорного» находится около Глебовского моста при входе в Кремль" },
            new { Image = "Гриб_художник.webp", Name = "Гриб художник", Location = "Неподалеку от дозороного на набережной Кремля разместился «Гриб-художник», рисующий рязанский пейзаж." },
            new { Image = "гриб_рыбак.webp", Name = "Гриб Рыбак", Location = "Самым целеустремленным собирателям стоит отправиться на берег пруда Родная заводь, неподалеку от школы №69 в микрорайоне Канищево" },
            new { Image = "гриб_мудрец.webp", Name = "Гриб мудрец", Location = "находится справа от входа в Центральную городскую библиотеку имени Сергея Есенина (Первомайский просп., 74к1)" },
            new { Image = "гриб_пионер.webp", Name = "Гриб Пионер", Location = "Вспомнить свои детские мечты предлагает «Гриб-пионер», спрятавшийся в парке Дворца детского творчества" },
            new { Image = "грибы_с_глазами.webp", Name = "Семейство «Грибов с глазами»", Location = "— бородатый гриб-отец и его два сынишки — отдыхает в окружении прочей лесной живности в Нижнем городском саду", FullWidth = true }
        };

        sb.Append("""
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
""");

        // Динамическая генерация карточек грибов
        foreach (var mushroom in mushrooms)
        {
            var cardClass = mushroom.FullWidth ? "tour-mushroom-card full-width" : "tour-mushroom-card";
            sb.Append($"""
        <div class='{cardClass}'>
            <div class='tour-mushroom-photo'>
                <img src='/imgs/mushrooms/{mushroom.Image}' alt='{mushroom.Name}'>
            </div>
            <h3 class='tour-mushroom-name'>{mushroom.Name}</h3>
            <p class='tour-mushroom-location'>{mushroom.Location}</p>
        </div>
""");
        }

        sb.Append("""
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
                    <button class='tour-btn-select' onclick='document.getElementById("fileInput").click()'>
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
""");

        await response.WriteAsync(sb.ToString(), cancellationToken);

        sb.Clear();
        sb.Append(TagJs(response, "mushroom.js"));
        sb.Append(TagJs(response, "bootstrap.js"));
        await response.WriteAsync(sb.ToString(), cancellationToken);

        await End(request, response, cancellationToken);
    }

    public override string Title => "Собирательство";
}