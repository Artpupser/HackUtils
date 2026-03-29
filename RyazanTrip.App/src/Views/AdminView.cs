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

        var sb = Builder;
        sb.Append("""
<div class="tour-admin-container">
    <div class="tour-admin-card">
        <div class="tour-admin-header">
            <h2 class="tour-admin-title">Создание Тура</h2>
        </div>
        <form id="createTourForm" class="tour-admin-form"  method="post" enctype="multipart/form-data">
            <div class="tour-form-row">
                <label class="tour-form-label">Название Тура:</label>
                <input name="name" type="text" class="tour-form-input" name="tourName" required>
            </div>

            <div class="tour-form-row">
                <label class="tour-form-label">Описание Тура:</label>
                <textarea name="description" class="tour-form-input tour-form-textarea" name="tourDescription" rows="3" required></textarea>
            </div>

            <div class="tour-form-row">
                <label class="tour-form-label">Дата Тура:</label>
                <input name="date" type="date" class="tour-form-input" name="tourDate" required>
            </div>

            <div class="tour-form-row">
                <label class="tour-form-label">Список Точек Тура:</label>
                <input name="coords_list_str" type="text" class="tour-form-input" name="tourPoints" placeholder="Точка 1, Точка 2, Точка 3..." required>
            </div>

            <div class="tour-form-row">
                <label class="tour-form-label">Картинка Тура:</label>
                <div class="tour-file-button-wrapper">
                    <input type="file" class="" name="image" accept="image/*" id="tourImageInput">
                    <span class="tour-file-name" id="fileNameDisplay">Выбор изображения</span>
                </div>
            </div>

            <div class="tour-form-row">
                <label class="tour-form-label">Цена Тура:</label>
                <div class="tour-price-input">
                    <input name="price" type="number" class="tour-form-input" name="tourPrice" min="0" placeholder="0" required>
                    <span class="tour-currency">₽</span>
                </div>
            </div>

            <div class="tour-form-actions">
                <button type="submit" class="tour-btn-create">
                    Создать!
                </button>
            </div>
        </form>
    </div>
</div>
""");
        sb.Append(TagJs(response, "admin.js"));
        await End(request, response, cancellationToken);
    }

    public override string Title => "Админ панель";
}