using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;
using System.Text;

namespace RyazanTrip.App.Views;

public sealed class PayTourView : View
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
        await Start(request, response, string.Empty, cancellationToken);

        var sb = new StringBuilder();

        sb.Append("""
<!-- СТРАНИЦА ТУРА -->
<div class='tour-service-page'>
    <div class='tour-tour-page'>
        <div class='tour-tour-card-main'>
            <h1 class='tour-tour-title'>Обзорная по Рязанскому кремлю!</h1>
            
            <div class='tour-tour-main'>
                <div class='tour-tour-image-wrapper'>
                    <img src='/api/public/files?name=rzn_kremlin_tour.webp' 
                         alt='Рязанский кремль' 
                         class='tour-tour-image'>
                </div>
                
                <div class='tour-tour-info-main'>
                    <p class='tour-tour-date-main'>Дата: 23.03.2026</p>
                    
                    <h2 class='tour-tour-subtitle'>Тур «Сердце Рязани: Кремль»</h2>
                    
                    <p class='tour-tour-description'>
                        Рязанский кремль — это не просто главная достопримечательность города. 
                        Это место, с которого начиналась история рязанской земли. Мы приглашаем вас 
                        на прогулку, где древние стены заговорят, а прошлое станет ближе.
                    </p>
                    
                    <div class='tour-tour-formats'>
                        <h4>Форматы</h4>
                        <ul>
                            <li>Индивидуальная экскурсия</li>
                            <li>Групповая (до 10 человек)</li>
                            <li>Семейный формат с элементами квеста для детей</li>
                        </ul>
                    </div>
                    
                    <p class='tour-tour-duration'>Продолжительность — 1,5–2 часа</p>
                    
                    <div class='tour-tour-footer'>
                        <button class='tour-btn-book-main' onclick='openBookingModal()'>
                            Забронировать
                        </button>
                    </div>
                </div>
            </div>
            
            <div class='tour-tour-expect'>
                <h3>Что вас ждет</h3>
                <p class='tour-expect-intro'>
                    Мы пройдем по территории одного из старейших музеев-заповедников России. Вы увидите:
                </p>
                <ul class='tour-expect-list'>
                    <li>Успенский собор — шедевр русского барокко с уникальным резным иконостасом</li>
                    <li>Архитектурный ансамбль кремля: Глебовский собор, колокольню, дворцы Олега и архиерейские палаты</li>
                    <li>Место, где когда-то стоял первый рязанский детинец, основанный еще в XI веке</li>
                    <li>Панорамные точки с лучшими видами на кремль и набережную Трубежа</li>
                </ul>
            </div>
        </div>
    </div>
</div>

<!-- МОДАЛЬНОЕ ОКНО БРОНИРОВАНИЯ -->
<div id='booking-modal' class='tour-modal-overlay' onclick='closeBookingModal(event)'>
    <div class='tour-booking-modal-window' onclick='event.stopPropagation()'>
        <button class='tour-modal-close' onclick='closeBookingModal()'>
            <i class='bi bi-x-lg'></i>
        </button>
        
        <div class='tour-booking-header'>
            <h2 class='tour-booking-title'>Обзорная по Рязанскому кремлю!</h2>
            <p class='tour-booking-date'>Дата: 23.03.2026</p>
        </div>

        <div class='tour-booking-content'>
            <div class='tour-booking-details'>
                <h3>Организационные детали</h3>
                <ul class='tour-details-list'>
                    <li><strong>Формат:</strong> Пешеходная экскурсия по территории и соборам.</li>
                    <li><strong>Длительность:</strong> от 1.5 до 2 часов.</li>
                    <li><strong>Билеты:</strong> Вход на территорию бесплатный, билеты в музеи и на экспозиции оплачиваются отдельно!</li>
                    <li><strong>Место встречи:</strong> У Соборной колокольни (памятник Олегу Рязанскому).</li>
                </ul>
            </div>

            <div class='tour-booking-services'>
                <h3 class='tour-services-title'>Услуги Гида:</h3>
                
                <button type='button' class='tour-btn-guide' onclick='selectGuide(this, true)' id='guide-yes'>
                    <span class='tour-btn-text'>Я пойду с гидом!</span>
                </button>
                
                <button type='button' class='tour-btn-guide' onclick='selectGuide(this, false)' id='guide-no'>
                    <span class='tour-btn-text'>Гид не требуется</span>
                </button>
            </div>
        </div>

        <div class='tour-booking-footer'>
            <button class='tour-btn-book' onclick='processBooking()'>
                Оплатить!
            </button>
        </div>
    </div>
</div>

<script src='/js/service.js'></script>
""");

        await response.WriteAsync(sb.ToString(), cancellationToken);

        sb.Clear();
        sb.Append(TagJs(response, "service.js"));
        sb.Append(TagJs(response, "bootstrap.js"));
        await response.WriteAsync(sb.ToString(), cancellationToken);

        await End(request, response, cancellationToken);
    }

    public override string Title => "Бронирование тура";
}