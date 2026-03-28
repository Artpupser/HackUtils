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
        await Start(request, response, string.Empty, cancellationToken);

        var sb = Builder;
        sb.Append("""
<div class="admin-page">
    
    <!-- КОНТЕЙНЕР -->
    <div class="admin-container">
        
        <!-- ЗАГОЛОВОК -->
        <h1 class="admin-title">Список Заявок</h1>

        <!-- СЕТКА ЗАЯВОК -->
        <div class="requests-grid">
            
            <!-- ЗАЯВКА 1 -->
            <div class="request-card" onclick="openModal(1)">
                <h3 class="request-number">Заявка 1:</h3>
                <p class="request-info"><strong>Пользователь:</strong> Макарова Алиса</p>
                <p class="request-info"><strong>Дата Загрузки:</strong> 22.03.2026</p>
                <p class="request-info"><strong>Гриб:</strong> Влюбленная Пара</p>
                
                <div class="request-photo">
                    <span>Фото:</span>
                    <img src="https://images.unsplash.com/photo-1569949381669-ecf31ae8e613?w=200" alt="Гриб">
                </div>
                
                <div class="request-action">
                    <span>Открыть</span>
                    <i class="bi bi-box-arrow-up-right"></i>
                </div>
            </div>
        </div>
    </div>
</div>

<!-- ==================== МОДАЛЬНОЕ ОКНО ==================== -->
<!-- Фон затемнения -->
<div id="modal-overlay" class="modal-overlay" onclick="closeModal()">
    
    <!-- Само окно -->
    <div class="modal-window" onclick="event.stopPropagation()">
        
        <!-- Заголовок -->
        <h3 class="modal-title">Заявка 1:</h3>
        
        <!-- Информация -->
        <p class="modal-info"><strong>Пользователь:</strong> Макарова Алиса</p>
        <p class="modal-info"><strong>Дата Загрузки:</strong> 22.03.2026</p>
        <p class="modal-info"><strong>Гриб:</strong> Влюбленная Пара</p>
        
        <!-- Фото -->
        <div class="modal-photo-section">
            <span>Фото:</span>
            <img id="modal-image" src="" alt="Фото гриба">
        </div>
        
        <!-- Кнопки действий -->
        <div class="modal-actions">
            <!-- Кнопка Подтвердить (зеленая) -->
            <button class="btn-confirm" onclick="confirmRequest()">
                <i class="bi bi-check-lg"></i>
            </button>
            
            <!-- Кнопка Отклонить (красная) -->
            <button class="btn-reject" onclick="rejectRequest()">
                <i class="bi bi-x-lg"></i>
            </button>
        </div>

    </div>
</div>
""");
        sb.Append(TagJs(response, "admin.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Админ панель";
}