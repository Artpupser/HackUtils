// Открыть модальное окно
        function openBookingModal() {
            const modal = document.getElementById('booking-modal');
            modal.classList.add('active');
            document.body.style.overflow = 'hidden'; // Блокируем прокрутку страницы
        }

        // Закрыть модальное окно
        function closeBookingModal(event) {
            // Если event передан (клик по overlay) и клик был внутри окна — не закрываем
            if (event && event.target.closest('.tour-booking-modal-window')) {
                return;
            }
            
            const modal = document.getElementById('booking-modal');
            modal.classList.remove('active');
            document.body.style.overflow = ''; // Возвращаем прокрутку
        }

        // Выбор гида
        function selectGuide(button, withGuide) {
            // Снимаем активность со всех кнопок
            document.querySelectorAll('.tour-btn-guide').forEach(btn => {
                btn.classList.remove('active');
            });
            
            // Активируем выбранную
            button.classList.add('active');
            
            // Сохраняем выбор
            window.selectedGuideOption = withGuide;
        }

        // Обработка бронирования
        function processBooking() {
            if (window.selectedGuideOption === undefined) {
                alert('Пожалуйста, выберите услуги гида!');
                return;
            }
            
            const message = window.selectedGuideOption 
                ? 'Вы выбрали тур с гидом. Переходим к оплате...' 
                : 'Вы выбрали тур без гида. Переходим к оплате...';
            
            alert(message);
            // Здесь можно добавить редирект на страницу оплаты
        }

        // Закрытие по Escape
        document.addEventListener('keydown', function(e) {
            if (e.key === 'Escape') {
                closeBookingModal();
            }
        });

        // Инициализация
        document.addEventListener('DOMContentLoaded', function() {
            console.log('Tour page loaded');
        });