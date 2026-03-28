// Инициализация карты
var map = L.map('map').setView([54.629,39.741],13);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',{
 attribution:'© OpenStreetMap contributors'
}).addTo(map);

let userCoords=null;
let currentTarget=1;
let scanner=null;
let lastCoords=null;

// Данные грибов
var mushrooms=[
 {name:"Рязанский коробейник", coords:[54.6269,39.7425], img:"imgs/mushrooms/гриб_коробейник.webp"},
 {name:"Мужичок-боровичок", coords:[54.6272,39.7428], img:"imgs/mushrooms/гри_мужичок_боровичок.webp"},
 {name:"Влюбленная пара", coords:[54.6275,39.7432], img:"imgs/mushrooms/грибная_пара.webp"},
 {name:"Грибная капелла", coords:[54.6258,39.7472], img:"imgs/mushrooms/грибная_капелла.webp"},
 {name:"Грибная команда", coords:[54.6263,39.7418], img:"imgs/mushrooms/грибная_команда.webp"},
 {name:"Гриб путешественник", coords:[54.6330,39.7450], img:"imgs/mushrooms/гриб_путешственник.webp"},
 {name:"Гриб профессор", coords:[54.6335,39.7445], img:"imgs/mushrooms/гриб_профессор.webp"},
 {name:"Гриб дозорный", coords:[54.6351,39.7469], img:"imgs/mushrooms/гриб_дозорный.webp"},
 {name:"Гриб художник", coords:[54.6355,39.7478], img:"imgs/mushrooms/Гриб_художник.webp"},
 {name:"Гриб Рыбак", coords:[54.6340,39.7520], img:"imgs/mushrooms/гриб_рыбак.webp"},
 {name:"Гриб Мудрец", coords:[54.6322,39.7454], img:"imgs/mushrooms/гриб_мудрец.webp"},
 {name:"Гриб Пионер", coords:[54.6300,39.7385], img:"imgs/mushrooms/гриб_пионер.webp"},
 {name:"Семейство «Грибов с глазами»", coords:[54.6295,39.7448], img:"imgs/mushrooms/грибы_с_глазами.webp"}
];

// Опыт за каждый гриб
let mushroomExp = new Array(mushrooms.length).fill(0);

// Старт квеста
document.getElementById("startBtn").onclick=()=>{
 navigator.geolocation.getCurrentPosition(pos=>{
   userCoords=[pos.coords.latitude,pos.coords.longitude];
   lastCoords = userCoords; 
   document.getElementById("startBtn").style.display="none";
   buildRouteToNext();
 });
};

// Построение маршрута
let routeLine=null;
function buildRouteToNext(){
 let next = mushrooms[currentTarget-1];
 document.getElementById("infoBox").innerHTML="Идите к грибу:<br><b>"+next.name+"</b>";

 if(routeLine) map.removeLayer(routeLine);

 routeLine = L.polyline([lastCoords,next.coords],{color:"green",weight:6}).addTo(map);

 // Иконка с картинкой
 var icon = L.icon({
   iconUrl: next.img,
   iconSize:[50,50],
   iconAnchor:[25,50],
   popupAnchor:[0,-50]
 });

 var marker = L.marker(next.coords,{icon:icon}).addTo(map);
 marker.bindPopup(`<b>${next.name}</b><br><img src="${next.img}" width="150"><br><button onclick="openScanner()">Сканировать QR</button>`);

 // Маркер для точки старта
 L.marker(lastCoords).addTo(map).bindPopup("Отсюда").openPopup();

 map.fitBounds([lastCoords,next.coords]);

 lastCoords = next.coords;
}

// QR сканер
function openScanner(){
 console.log("Открываем сканер");
 document.getElementById("scannerModal").style.display="flex";
 scanner = new Html5QrcodeScanner("scanner",{fps:10,qrbox:250});
 try {
   scanner.render(onScanSuccess);
   console.log("Сканер рендерится");
 } catch (error) {
   console.error("Ошибка при рендере сканера:", error);
 }
}

document.getElementById("closeScanner").onclick=()=>{
 if(scanner) scanner.clear();
 document.getElementById("scannerModal").style.display="none";
}

function onScanSuccess(text){
 console.log("QR считан:",text);
 if(scanner) scanner.clear();
 document.getElementById("scannerModal").style.display="none";

 currentTarget++;
 mushroomExp[currentTarget - 2] = 30;
 alert(`Опыт за гриб "${mushrooms[currentTarget - 2].name}": ${mushroomExp[currentTarget - 2]}`);
 alert(`Общий опыт по грибам: ${mushroomExp.join(', ')}`);

 if(currentTarget>mushrooms.length){
   showFinalModal();
   return;
 }

 alert("QR считан! Строим маршрут к следующему грибу 🚶‍♂️");
 buildRouteToNext();
}

// Финальное окно
function showFinalModal(){
 document.getElementById("finalModal").style.display="flex";
}

// Кнопка "За наградой!"
document.getElementById("rewardBtn").onclick=()=>{
 window.location.href="https://yourwebsite.com/reward"; // <-- замените на свою ссылку
}