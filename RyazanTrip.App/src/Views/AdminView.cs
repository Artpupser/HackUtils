using PupaMVCF.Framework.Core;
using PupaMVCF.Framework.Views;
using RyazanTrip.App.Components;
using System.Text;

namespace RyazanTrip.App.Views;

public sealed class AdminView : View
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

        sb.Append("""
<div class='tour-admin-page'>
    <div class='tour-admin-container'>

        <h1 class='tour-admin-title'>Список Заявок</h1>

        <div class='tour-requests-grid' id='requestsGrid'>

            <div class='tour-request-card' data-id='1' onclick='openModal(1)'>
                <h3 class='tour-request-number'>Заявка 1:</h3>
                <p class='tour-request-info'><strong>Пользователь:</strong> Макарова Алиса</p>
                <p class='tour-request-info'><strong>Дата Загрузки:</strong> 22.03.2026</p>
                <p class='tour-request-info'><strong>Гриб:</strong> Влюбленная Пара</p>

                <div class='tour-request-photo'>
                    <span>Фото:</span>
                    <img src='/api/public/files?name=грибная_пара.webp' alt='Гриб'>
                </div>

                <div class='tour-request-action'>
                    <span>Открыть</span>
                    <i class='bi bi-box-arrow-up-right'></i>
                </div>
            </div>

            <div class='tour-request-card' data-id='2' onclick='openModal(2)'>
                <h3 class='tour-request-number'>Заявка 2:</h3>
                <p class='tour-request-info'><strong>Пользователь:</strong> Андрюшин Артем</p>
                <p class='tour-request-info'><strong>Дата Загрузки:</strong> 23.03.2026</p>
                <p class='tour-request-info'><strong>Гриб:</strong> Боровик</p>

                <div class='tour-request-photo'>
                    <span>Фото:</span>
                    <img src='/api/public/files?name=гри_мужичок_боровичок.webp' alt='Гриб'>
                </div>

                <div class='tour-request-action'>
                    <span>Открыть</span>
                    <i class='bi bi-box-arrow-up-right'></i>
                </div>
            </div>

            <div class='tour-request-card' data-id='3' onclick='openModal(3)'>
                <h3 class='tour-request-number'>Заявка 3:</h3>
                <p class='tour-request-info'><strong>Пользователь:</strong> Филимонова Анна</p>
                <p class='tour-request-info'><strong>Дата Загрузки:</strong> 24.03.2026</p>
                <p class='tour-request-info'><strong>Гриб:</strong> Подосиновик</p>

                <div class='tour-request-photo'>
                    <span>Фото:</span>
                    <img src='/api/public/files?name=грибная_капелла.webp' alt='Гриб'>
                </div>

                <div class='tour-request-action'>
                    <span>Открыть</span>
                    <i class='bi bi-box-arrow-up-right'></i>
                </div>
            </div>

            <div class='tour-request-card' data-id='4' onclick='openModal(4)'>
                <h3 class='tour-request-number'>Заявка 4:</h3>
                <p class='tour-request-info'><strong>Пользователь:</strong> Хандина Анна</p>
                <p class='tour-request-info'><strong>Дата Загрузки:</strong> 25.03.2026</p>
                <p class='tour-request-info'><strong>Гриб:</strong> Лисички</p>

                <div class='tour-request-photo'>
                    <span>Фото:</span>
                    <img src='/api/public/files?name=грибная_капелла.webp' alt='Гриб'>
                </div>

                <div class='tour-request-action'>
                    <span>Открыть</span>
                    <i class='bi bi-box-arrow-up-right'></i>
                </div>
            </div>

        </div>

        <div id='noRequestsMsg' class='no-requests' style='display: none;'>
            <i class='bi bi-check-circle-fill' style='font-size: 48px; color: var(--tour-green); margin-bottom: 15px; display: block;'></i>
            Все заявки обработаны!
        </div>

    </div>
</div>

<div id='tour-modal-overlay' class='tour-modal-overlay' onclick='closeModal(event)'>
    <div class='tour-modal-window admin-modal' onclick='event.stopPropagation()'>

        <button class='admin-modal-close' onclick='closeModal()'>
            <i class='bi bi-x-lg'></i>
        </button>

        <div class='admin-modal-content'>
            <h3 class='tour-modal-title' id='modalTitle'>Заявка 1:</h3>

            <p class='tour-modal-info' id='modalUser'><strong>Пользователь:</strong> Макарова Алиса</p>
            <p class='tour-modal-info' id='modalDate'><strong>Дата Загрузки:</strong> 22.03.2026</p>
            <p class='tour-modal-info' id='modalMushroom'><strong>Гриб:</strong> Влюбленная Пара</p>

            <div class='admin-modal-photo'>
                <img id='tour-modal-image' src='' alt='Фото гриба'>
            </div>

            <div class='admin-modal-actions'>
                <button class='tour-btn-confirm' onclick='confirmRequest()' title='Подтвердить'>
                    <i class='bi bi-check-lg'></i>
                </button>
                <button class='tour-btn-reject' onclick='rejectRequest()' title='Отклонить'>
                    <i class='bi bi-x-lg'></i>
                </button>
            </div>
        </div>

    </div>
</div>
""");
        sb.Append(TagJs(response, "admin.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Админ панель";
}