const slides = document.querySelector('.slides')
const slide = document.querySelectorAll('.slide')

const prev = document.querySelector('.prev')
const next = document.querySelector('.next')

let index = 0
const width = 500

function updateSlider(){
  slides.style.transform = `translateX(-${index * width}px)`
}

next.addEventListener('click', () => {
  index++
  if(index >= slide.length) index = 0
  updateSlider()
})

prev.addEventListener('click', () => {
  index--
  if(index < 0) index = slide.length - 1
  updateSlider()
})