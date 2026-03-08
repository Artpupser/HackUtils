const slider = document.getElementById("slider");
const value = document.getElementById("value");

slider.addEventListener("input", function () {
  value.textContent = slider.value;
});