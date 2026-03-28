using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;

namespace RyazanTrip.App.Views;

public sealed class MushroomQuestView : View
{
    #region START_END

    protected override async Task Start(Request request, Response response, string stylesForBody, CancellationToken cancellationToken)
    {
        await base.Start(request, response, stylesForBody, cancellationToken);
        await new HeaderComponent(this).Html(request, response, cancellationToken);
    }

    protected override async Task End(Request request, Response response, CancellationToken cancellationToken)
    {
        await base.End(request, response, cancellationToken);
        await new FooterComponent(this).Html(request, response, cancellationToken);
    }

    #endregion

    public override async Task Html(Request request, Response response, CancellationToken cancellationToken)
    {
        // Вставка шапки
        await Start(request, response, "tour-mushroom-page", cancellationToken);

        var sb = new System.Text.StringBuilder();

        sb.Append(@"
<div class=""tour-mushroom-page"">
    
    <!-- ШАПКА -->
    <div class=""tour-mushroom-header"">
        <h1 class=""tour-mushroom-title"">Грибной квест</h1>
        <p class=""tour-mushroom-subtitle"">
            Собирай грибы — получай бонусы!<br>
            Гуляя по разным уголкам нашего города найди памятники-грибы. 
            Сделай фото с ними и загружай в личный кабинет. 
            <strong>50 баллов за каждый найденный гриб!</strong>
        </p>
    </div>

    <!-- СЕТКА ГРИБОВ -->
    <div class=""tour-mushroom-grid"">
        
        <!-- Гриб 1 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_коробейник.webp"" 
                     alt=""Рязанский коробейник"">
            </div>
            <h3 class=""tour-mushroom-name"">Рязанский коробейник</h3>
            <p class=""tour-mushroom-location"">
                Зазывает горожан неподолеку от торговых рядов на площади ленина
            </p>
        </div>

        <!-- Гриб 2 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гри_мужичок_боровичок.webp"" 
                     alt=""Мужичок-боровичок"">
            </div>
            <h3 class=""tour-mushroom-name"">Мужичок-боровичок</h3>
            <p class=""tour-mushroom-location"">
                расположился на главной пешеходной улице рязани - почтовой
            </p>
        </div>

        <!-- Гриб 3 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=грибная_пара.webp"" 
                     alt=""Влюбленная пара"">
            </div>
            <h3 class=""tour-mushroom-name"">Влюбленная пара</h3>
            <p class=""tour-mushroom-location"">
                Прячется под зонтиком от капель фонтана на площади театральной
            </p>
        </div>

        <!-- Гриб 4 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=грибная_капелла.webp"" 
                     alt=""Грибная капелла"">
            </div>
            <h3 class=""tour-mushroom-name"">Грибная капелла</h3>
            <p class=""tour-mushroom-location"">
                поющая команда расположилась на площади победы
            </p>
        </div>

        <!-- Гриб 5 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=грибная_команда.webp"" 
                     alt=""Грибная Команда"">
            </div>
            <h3 class=""tour-mushroom-name"">Грибная команда</h3>
            <p class=""tour-mushroom-location"">
                По Лыбедскому бульвару озорные лисички катаются с горки на скейте.
            </p>
        </div>

        <!-- Гриб 6 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_путешственник.webp"" 
                     alt=""Гриб Путешественник"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб путешественник</h3>
            <p class=""tour-mushroom-location"">
                с рюкзаком за плечами увлеченно изучает карту на первомайском проспекте 
            </p>
        </div>

        <!-- Гриб 7 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_профессор.webp"" 
                     alt=""Гриб Профессор"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб профессор</h3>
            <p class=""tour-mushroom-location"">
                читает лекцию своим студентам. Скульптура расположилась под окнами Рязанского Политеха на улице Ленина, 53.
            </p>
        </div>

        <!-- Гриб 8 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_дозорный.webp"" 
                     alt=""Гриб дозорный"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб дозорный</h3>
            <p class=""tour-mushroom-location"">
                Пост «Гриба-дозорного» находится около Глебовского моста при входе в Кремль
            </p>
        </div>

        <!-- Гриб 9 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=Гриб_художник.webp"" 
                     alt=""Гриб художник"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб художник</h3>
            <p class=""tour-mushroom-location"">
                Неподалеку от дозороного на набережной Кремля разместился «Гриб-художник», рисующий рязанский пейзаж.
            </p>
        </div>

        <!-- Гриб 10 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_рыбак.webp"" 
                     alt=""Гриб Рыбак"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб Рыбак</h3>
            <p class=""tour-mushroom-location"">
                Самым целеустремленным собирателям стоит отправиться на берег пруда Родная заводь, неподалеку от школы №69 в микрорайоне Канищево
            </p>
        </div>

        <!-- Гриб 11 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_мудрец.webp"" 
                     alt=""Гриб Мудрец"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб мудрец</h3>
            <p class=""tour-mushroom-location"">
                находится справа от входа в Центральную городскую библиотеку имени Сергея Есенина (Первомайский просп., 74к1)
            </p>
        </div>

        <!-- Гриб 12 -->
        <div class=""tour-mushroom-card"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=гриб_пионер.webp"" 
                     alt=""Гриб пионер"">
            </div>
            <h3 class=""tour-mushroom-name"">Гриб Пионер</h3>
            <p class=""tour-mushroom-location"">
                Вспомнить свои детские мечты предлагает «Гриб-пионер», спрятавшийся в парке Дворца детского творчества
            </p>
        </div>

        <!-- Гриб 13 -->
        <div class=""tour-mushroom-card full-width"">
            <div class=""tour-mushroom-photo"">
                <img src=""/api/public/files?name=грибы_с_глазами.webp"" 
                     alt=""Семейство «Грибов с глазами»"">
            </div>
            <h3 class=""tour-mushroom-name"">Семейство «Грибов с глазами»</h3>
            <p class=""tour-mushroom-location"">
                 — бородатый гриб-отец и его два сынишки — отдыхает в окружении прочей лесной живности в Нижнем городском саду
            </p>
        </div>
    </div>
    
    <div class=""position-btn"" style=""text-align:center; margin-top:20px;"">
        <button class=""tour-btn-book"" id=""startMushroomQuestBtn"">
            ЗА ГРИБАМИ!
        </button>
    </div>
</div>

<script>
    document.getElementById(""startMushroomQuestBtn"").onclick = function(){
        window.location.href = ""/map""; 
    };
</script>

");


        sb.Append(TagJs(response,"app.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Грибной квест - Ryazan Trip";
}