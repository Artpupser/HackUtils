// Данные заявок
const requestsData = {
    1: {
        id: 1,
        user: 'Макарова Алиса',
        date: '22.03.2026',
        mushroom: 'Влюбленная Пара',
        photo: 'https://avatars.mds.yandex.net/i?id=5123b89fe98d2da31adb104b34b0b782_l-9151051-images-thumbs&n=13'
    },
    2: {
        id: 2,
        user: 'Андрюшин Артем',
        date: '23.03.2026',
        mushroom: 'Боровик',
        photo: 'https://avatars.mds.yandex.net/get-altay/16136925/2a00000197bf8ad90b821849fc61bd37a9e6/XXL_height'
    },
    3: {
        id: 3,
        user: 'Филимонова Анна',
        date: '24.03.2026',
        mushroom: 'Подосиновик',
        photo: 'https://avatars.mds.yandex.net/i?id=edb551da60c5f05959dd16b67394cc51af3bf131-12610597-images-thumbs&n=13'
    },
    4: {
        id: 4,
        user: 'Хандина Анна',
        date: '25.03.2026',
        mushroom: 'Лисички',
        photo: 'https://avatars.mds.yandex.net/i?id=398769c0e4a22c63dfff8b4d2ecbeda9fd806d69-4835468-images-thumbs&n=13'
    }
};

let currentRequestId = null;

/**
 * Открыть модальное окно
 */
function openModal(id) {
    currentRequestId = id;
    const data = requestsData[id];
    
    if (!data) return;
    
    // Заполняем данные
    document.getElementById('modalTitle').textContent = `Заявка ${data.id}:`;
    document.getElementById('modalUser').innerHTML = `<strong>Пользователь:</strong> ${data.user}`;
    document.getElementById('modalDate').innerHTML = `<strong>Дата Загрузки:</strong> ${data.date}`;
    document.getElementById('modalMushroom').innerHTML = `<strong>Гриб:</strong> ${data.mushroom}`;
    document.getElementById('tour-modal-image').src = data.photo;
    
    // Показываем модалку
    const modal = document.getElementById('tour-modal-overlay');
    modal.classList.add('active');
    document.body.style.overflow = 'hidden';
}

/**
 * Закрыть модальное окно
 */
function closeModal(event) {
    // Если event передан (клик по overlay) и клик был внутри окна — не закрываем
    if (event && event.target.closest('.admin-modal')) {
        return;
    }
    
    const modal = document.getElementById('tour-modal-overlay');
    modal.classList.remove('active');
    document.body.style.overflow = '';
    currentRequestId = null;
}

/**
 * Удалить карточку заявки из сетки
 */
function removeRequestCard(id) {
    const card = document.querySelector(`.tour-request-card[data-id="${id}"]`);
    if (card) {
        // Добавляем класс для анимации
        card.classList.add('removing');
        
        // Удаляем после анимации
        setTimeout(() => {
            card.remove();
            checkEmptyState();
        }, 300);
    }
    
    // Удаляем из данных
    delete requestsData[id];
}

/**
 * Проверить, остались ли заявки
 */
function checkEmptyState() {
    const remainingCards = document.querySelectorAll('.tour-request-card');
    const noRequestsMsg = document.getElementById('noRequestsMsg');
    const grid = document.getElementById('requestsGrid');
    
    if (remainingCards.length === 0) {
        grid.style.display = 'none';
        noRequestsMsg.style.display = 'block';
    }
}

/**
 * Подтвердить заявку
 */
function confirmRequest() {
    if (!currentRequestId) return;
    
    // Удаляем карточку
    removeRequestCard(currentRequestId);
    
    // Закрываем модалку
    closeModal();
    
    // Уведомление (можно убрать если мешает)
    console.log(`Заявка ${currentRequestId} подтверждена`);
}

/**
 * Отклонить заявку
 */
function rejectRequest() {
    if (!currentRequestId) return;
    
    if (confirm(`Вы уверены, что хотите отклонить заявку ${currentRequestId}?`)) {
        // Удаляем карточку
        removeRequestCard(currentRequestId);
        
        // Закрываем модалку
        closeModal();
        
        console.log(`Заявка ${currentRequestId} отклонена`);
    }
}

// Закрытие по Escape
document.addEventListener('keydown', function(e) {
    if (e.key === 'Escape') {
        closeModal();
    }
});

// Инициализация
document.addEventListener('DOMContentLoaded', function() {
    console.log('Admin panel loaded');
});