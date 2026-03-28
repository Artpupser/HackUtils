function goToUpload() {
            // Переход на страницу загрузки фото или открытие модалки
            window.location.href = 'profile.html#upload';
            // Или можно открыть модальное окно для загрузки
        }
        
        // Анимация появления карточек при скролле
        document.addEventListener('DOMContentLoaded', function() {
            const cards = document.querySelectorAll('.tour-mushroom-card');
            
            const observer = new IntersectionObserver((entries) => {
                entries.forEach((entry, index) => {
                    if (entry.isIntersecting) {
                        setTimeout(() => {
                            entry.target.style.opacity = '1';
                            entry.target.style.transform = 'translateY(0)';
                        }, index * 100);
                    }
                });
            }, { threshold: 0.1 });
            
            cards.forEach(card => {
                card.style.opacity = '0';
                card.style.transform = 'translateY(20px)';
                card.style.transition = 'all 0.5s ease';
                observer.observe(card);
            });
        });
// Открытие модального окна при клике на фото
document.querySelectorAll('.tour-mushroom-photo').forEach(photo => {
    photo.addEventListener('click', function() {
        const modal = new bootstrap.Modal(document.getElementById('uploadModal'));
        modal.show();
        
        // Сохраняем ID гриба (можно добавить data-атрибут в HTML)
        const card = this.closest('.tour-mushroom-card');
        const mushroomName = card.querySelector('.tour-mushroom-name').textContent;
        document.getElementById('uploadModal').dataset.mushroom = mushroomName;
    });
});

// Drag & Drop
const dropZone = document.getElementById('dropZone');
const fileInput = document.getElementById('fileInput');

dropZone.addEventListener('click', () => fileInput.click());

dropZone.addEventListener('dragover', (e) => {
    e.preventDefault();
    dropZone.classList.add('drag-over');
});

dropZone.addEventListener('dragleave', () => {
    dropZone.classList.remove('drag-over');
});

dropZone.addEventListener('drop', (e) => {
    e.preventDefault();
    dropZone.classList.remove('drag-over');
    
    const files = e.dataTransfer.files;
    if (files.length > 0) {
        handleFile(files[0]);
    }
});

fileInput.addEventListener('change', (e) => {
    if (e.target.files.length > 0) {
        handleFile(e.target.files[0]);
    }
});

function handleFile(file) {
    if (!file.type.startsWith('image/')) {
        alert('Пожалуйста, выберите изображение');
        return;
    }
    
    const reader = new FileReader();
    reader.onload = (e) => {
        document.getElementById('imagePreview').src = e.target.result;
        document.getElementById('dropZone').classList.add('d-none');
        document.getElementById('previewContainer').classList.remove('d-none');
    };
    reader.readAsDataURL(file);
}

function submitPhoto() {
    const mushroomName = document.getElementById('uploadModal').dataset.mushroom;
    // Здесь отправка на сервер
    alert(`Фото с "${mushroomName}" отправлено! +50 баллов`);
    
    // Закрыть модал и сбросить
    bootstrap.Modal.getInstance(document.getElementById('uploadModal')).hide();
    resetModal();
}

// Сброс при закрытии модала
document.getElementById('uploadModal').addEventListener('hidden.bs.modal', resetModal);

function resetModal() {
    document.getElementById('dropZone').classList.remove('d-none');
    document.getElementById('previewContainer').classList.add('d-none');
    document.getElementById('fileInput').value = '';
    document.getElementById('imagePreview').src = '';
}