// =============================
// ИНИЦИАЛИЗАЦИЯ КАРТЫ
// =============================
var map = L.map('map').setView([54.629,39.741],13);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',{
 attribution:'© OpenStreetMap contributors'
}).addTo(map);

let userCoords=null;
let currentTarget=1;
let scanner=null;
let lastCoords=null;

// =============================
// ДАННЫЕ ГРИБОВ С ID
// =============================
var mushrooms=[
 {id:1,name:"Рязанский коробейник",coords:[54.6269,39.7425],img:"/api/public/files?name=гриб_коробейник.webp"},
 {id:2,name:"Мужичок-боровичок",coords:[54.6272,39.7428],img:"/api/public/files?name=гри_мужичок_боровичок.webp"},
 {id:3,name:"Влюбленная пара",coords:[54.6275,39.7432],img:"/api/public/files?name=грибная_пара.webp"},
 {id:4,name:"Грибная капелла",coords:[54.6258,39.7472],img:"/api/public/files?name=грибная_капелла.webp"},
 {id:5,name:"Грибная команда",coords:[54.6263,39.7418],img:"/api/public/files?name=грибная_команда.webp"},
 {id:6,name:"Гриб путешественник",coords:[54.6330,39.7450],img:"/api/public/files?name=гриб_путешственник.webp"},
 {id:7,name:"Гриб профессор",coords:[54.6335,39.7445],img:"environment/public/гриб_профессор.webp"},
 {id:8,name:"Гриб дозорный",coords:[54.6351,39.7469],img:"environment/public/гриб_дозорный.webp"},
 {id:9,name:"Гриб художник",coords:[54.6355,39.7478],img:"environment/public/Гриб_художник.webp"},
 {id:10,name:"Гриб Рыбак",coords:[54.6340,39.7520],img:"environment/public/гриб_рыбак.webp"},
 {id:11,name:"Гриб Мудррец",coords:[54.6322,39.7454],img:"environment/public/гриб_мудрец.webp"},
 {id:12,name:"Гриб Пионер",coords:[54.6300,39.7385],img:"environment/public/гриб_пионер.webp"},
 {id:13,name:"Семейство «Грибов с глазами»",coords:[54.6295,39.7448],img:"environment/public/грибы_с_глазами.webp"}
];

let mushroomExp = new Array(mushrooms.length).fill(0);

// =============================
// СТАРТ КВЕСТА
// =============================
document.getElementById("startBtn").onclick=()=>{
 navigator.geolocation.getCurrentPosition(pos=>{
   userCoords=[pos.coords.latitude,pos.coords.longitude];
   lastCoords=userCoords;
   document.getElementById("startBtn").style.display="none";
   buildRouteToNext();
 });
};

// =============================
// ПОСТРОЕНИЕ МАРШРУТА
// =============================
let routeLine=null;

function buildRouteToNext(){
 let next=mushrooms[currentTarget-1];
 document.getElementById("infoBox").innerHTML="Идите к грибу:<br><b>"+next.name+"</b>";

 if(routeLine) map.removeLayer(routeLine);

 routeLine=L.polyline([lastCoords,next.coords],{color:"green",weight:6}).addTo(map);

 var icon=L.icon({
   iconUrl:next.img,
   iconSize:[50,50],
   iconAnchor:[25,50]
 });

 var marker=L.marker(next.coords,{icon:icon}).addTo(map);
 marker.bindPopup(`<b>${next.name}</b><br><img src="${next.img}" width="150"><br><button onclick="openScanner()">Сканировать QR</button>`);

 L.marker(lastCoords).addTo(map).bindPopup("Отсюда").openPopup();

 map.fitBounds([lastCoords,next.coords]);
 lastCoords=next.coords;
}

// =============================
// ОТКРЫТИЕ СКАНЕРА (РАБОЧЕЕ)
// =============================
function openScanner(){
 document.getElementById("scannerModal").style.display="flex";

 scanner=new Html5Qrcode("scanner");

 Html5Qrcode.getCameras().then(cameras=>{
   if(!cameras.length){
     alert("Камера не найдена");
     return;
   }

   scanner.start(
     cameras[0].id,
     {fps:10, qrbox:250},
     onScanSuccess
   );
 }).catch(()=>{
   alert("Нет доступа к камере (нужен HTTPS или localhost)");
 });
}

// =============================
// ЗАКРЫТИЕ СКАНЕРА
// =============================
document.getElementById("closeScanner").onclick=()=>{
 if(scanner){
   scanner.stop().then(()=>scanner.clear());
 }
 document.getElementById("scannerModal").style.display="none";
};

// =============================
// ПРОВЕРКА QR КОДА 🍄
// QR должен содержать:
// {"type":"mushroom","id":3}
// =============================
function onScanSuccess(text){

 if(scanner){
   scanner.stop().then(()=>scanner.clear());
 }
 document.getElementById("scannerModal").style.display="none";

 let data;

 // QR не нашего формата
 try{
   data=JSON.parse(text);
 }catch{
   alert("❌ Это не QR грибного квеста");
   return;
 }

 if(data.type!=="mushroom" || !data.id){
   alert("❌ Неверный QR код");
   return;
 }

 let expected=mushrooms[currentTarget-1];

 // НЕ тот гриб
 if(data.id!==expected.id){
   alert("⚠️ Это не тот гриб!\nНайдите: "+expected.name);
   return;
 }

 // ПРАВИЛЬНЫЙ ГРИБ 🎉
 mushroomExp[currentTarget-1]=30;
 alert("🍄 Найден правильный гриб!\n+30 XP");

 currentTarget++;

 if(currentTarget>mushrooms.length){
   showFinalModal();
   return;
 }

 buildRouteToNext();
}

// =============================
// ФИНАЛ
// =============================
function showFinalModal(){
 document.getElementById("finalModal").style.display="flex";
}

document.getElementById("rewardBtn").onclick=()=>{
 window.location.href="RyazanTrip.App/src/Views/BonusesView.cs";
};