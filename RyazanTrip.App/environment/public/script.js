const slides = document.querySelectorAll('.slide');
const prev = document.querySelector('.prev');
const next = document.querySelector('.next');

let index = 0;

function showSlide(i) {
  slides.forEach((slide, idx) => {
    slide.style.display = idx === i ? 'flex' : 'none'; // показываем только текущий слайд
  });
}

// Кнопки
next.addEventListener('click', () => {
  index = (index + 1) % slides.length;
  showSlide(index);
});

prev.addEventListener('click', () => {
  index = (index - 1 + slides.length) % slides.length;
  showSlide(index);
});

// Инициализация
showSlide(index);