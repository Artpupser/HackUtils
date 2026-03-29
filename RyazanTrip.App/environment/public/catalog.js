// Массив с данными о турах (можно пополнять из админки)
let tours = [
    {
        id: 1,
        number: "Тур 1",
        name: "ОБЗОРНАЯ ПО РЯЗАНСКОМУ КРЕМЛЮ!",
        image: "https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/rzn_kremlin_tour.webp",
        date: "28.03.2026",
        price: "3.000.000₽",
        format: "Групповая (До 10ти Человек)"
    },
    {
        id: 2,
        number: "Тур 2",
        name: "ЗАБЕГ ПО УЛИЦЕ ПОЧТОВОЙ",
        image: "https://afc5e8a7-ce80-4666-abe8-61a18f87a6b2.selstorage.ru/pochtovaya_street_tour.webp",
        date: "21.03.2026",
        price: "1.000.000₽",
        format: "Индивидуальная"
    },
];

// Функция отрисовки всех туров
function renderTours() {
    const container = document.getElementById('toursContainer');
    const emptyState = document.getElementById('emptyState');
    
    // Очистка контейнера
    container.innerHTML = '';
    
    // Проверка на пустой массив
    if (tours.length === 0) {
        emptyState.style.display = 'flex';
        return;
    } else {
        emptyState.style.display = 'none';
    }
    
    // Генерация HTML для каждого тура
    tours.forEach((tour, index) => {
        const tourCard = document.createElement('div');
        tourCard.className = 'tour-catalog-card';
        tourCard.innerHTML = `
            <div class="tour-card-header">
                <span class="tour-card-number">${tour.number}:</span>
                <h3 class="tour-card-name">${tour.name}</h3>
            </div>
            
            <div class="tour-card-body">
                <div class="tour-card-image">
                    <img src="${tour.image}" alt="${tour.name}"">
                </div>
                
                <div class="tour-card-info">
                    <div class="tour-info-row">
                        <span class="tour-info-label">ДАТА:</span>
                        <span class="tour-info-value">${tour.date}</span>
                    </div>
                    <div class="tour-info-row">
                        <span class="tour-info-label">ЦЕНА:</span>
                        <span class="tour-info-value">${tour.price}</span>
                    </div>
                    <div class="tour-info-row">
                        <span class="tour-info-label">ФОРМАТ:</span>
                        <span class="tour-info-value">${tour.format}</span>
                    </div>
                </div>
                
                <div class="tour-card-actions">
                    <button class="tour-btn-more" onclick="openTourDetails(${tour.id})">
                        Узнать больше!
                    </button>
                </div>
            </div>
        `;
        
        // Анимация появления с задержкой
        tourCard.style.opacity = '0';
        tourCard.style.transform = 'translateY(20px)';
        container.appendChild(tourCard);
        
        setTimeout(() => {
            tourCard.style.transition = 'all 0.5s ease';
            tourCard.style.opacity = '1';
            tourCard.style.transform = 'translateY(0)';
        }, index * 100);
    });
}

// Функция добавления нового тура (для интеграции с admin.html)
function addTour(tourData) {
    const newId = tours.length > 0 ? Math.max(...tours.map(t => t.id)) + 1 : 1;
    const newTour = {
        id: newId,
        number: `Тур ${newId}`,
        ...tourData
    };
    tours.push(newTour);
    renderTours();
    return newTour;
}

// Функция удаления тура
function removeTour(id) {
    tours = tours.filter(t => t.id !== id);
    renderTours();
}

// Открытие деталей тура (заглушка)
function openTourDetails(id) {
    const tour = tours.find(t => t.id === id);
    if (tour) {
        // Переход на страницу тура или открытие модалки
        console.log('Открытие тура:', tour);
        // window.location.href = `service.html?id=${id}`;
        alert(`Вы выбрали: ${tour.name}\nДата: ${tour.date}\nЦена: ${tour.price}`);
    }
}

// Инициализация при загрузке
document.addEventListener('DOMContentLoaded', function() {
    renderTours();
    
    // Демонстрация: добавление тура по кнопке (для теста)
    console.log('Catalog loaded. Tours:', tours.length);
    console.log('Используйте addTour({name, image, date, price, format}) для добавления новых туров');
});

// Экспорт функций для использования в других скриптах (опционально)
window.TourCatalog = {
    add: addTour,
    remove: removeTour,
    getAll: () => tours,
    refresh: renderTours
};