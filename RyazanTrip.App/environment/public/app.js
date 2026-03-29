// =============================
// ИНИЦИАЛИЗАЦИЯ
// =============================
document.addEventListener('DOMContentLoaded', function () {

    // =============================
    // ГЛОБАЛЬНОЕ СОСТОЯНИЕ
    // =============================
    let map = L.map('map').setView([54.629, 39.741], 13);

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    let userCoords = null;
    let currentPosition = null;
    let currentTarget = 1;
    let scanner = null;
    let routeLine = null;

    // =============================
    // ГРИБЫ
    // =============================
    const mushrooms = [
        { id: 1, name: "Рязанский коробейник", coords: [54.6269, 39.7425], img: "/api/public/files?name=гриб_коробейник.webp" },
        { id: 2, name: "Мужичок-боровичок", coords: [54.6272, 39.7428], img: "/api/public/files?name=гри_мужичок_боровичок.webp" },
        { id: 3, name: "Влюбленная пара", coords: [54.6275, 39.7432], img: "/api/public/files?name=грибная_пара.webp" },
        { id: 4, name: "Грибная капелла", coords: [54.6258, 39.7472], img: "/api/public/files?name=грибная_капелла.webp" },
        { id: 5, name: "Грибная команда", coords: [54.6263, 39.7418], img: "/api/public/files?name=грибная_команда.webp" },
        { id: 6, name: "Гриб путешественник", coords: [54.6330, 39.7450], img: "/api/public/files?name=гриб_путешственник.webp" },
        { id: 7, name: "Гриб профессор", coords: [54.6335, 39.7445], img: "/api/public/files?name=гриб_профессор.webp" },
        { id: 8, name: "Гриб дозорный", coords: [54.6351, 39.7469], img: "/api/public/files?name=гриб_дозорный.webp" },
        { id: 9, name: "Гриб художник", coords: [54.6355, 39.7478], img: "/api/public/files?name=Гриб_художник.webp" },
        { id: 10, name: "Гриб Рыбак", coords: [54.6340, 39.7520], img: "/api/public/files?name=гриб_рыбак.webp" },
        { id: 11, name: "Гриб Мудрец", coords: [54.6322, 39.7454], img: "/api/public/files?name=гриб_мудрец.webp" },
        { id: 12, name: "Гриб Пионер", coords: [54.6300, 39.7385], img: "/api/public/files?name=гриб_пионер.webp" },
        { id: 13, name: "Семейство «Грибов с глазами»", coords: [54.6295, 39.7448], img: "/api/public/files?name=грибы_с_глазами.webp" }
    ];

    let mushroomExp = new Array(mushrooms.length).fill(0);

    // =============================
    // UI EVENTS (делегирование)
    // =============================
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('openScannerBtn')) {
            openScanner();
        }

        if (e.target.id === 'closeScanner' || e.target.classList.contains('closeScanner')) {
            stopScanner();
        }
    });

    // =============================
    // СТАРТ КВЕСТА
    // =============================
    document.getElementById("startBtn").onclick = () => {
        navigator.geolocation.getCurrentPosition(pos => {
            userCoords = [pos.coords.latitude, pos.coords.longitude];
            currentPosition = userCoords;

            document.getElementById("startBtn").style.display = "none";

            buildRouteToNext();
        });
    };

    // =============================
    // ПОСТРОЕНИЕ МАРШРУТА
    // =============================
    function buildRouteToNext() {
        const next = mushrooms[currentTarget - 1];

        document.getElementById("infoBox").innerHTML =
            "Идите к грибу:<br><b>" + next.name + "</b>";

        if (routeLine) map.removeLayer(routeLine);

        routeLine = L.polyline([currentPosition, next.coords], {
            color: "green",
            weight: 6
        }).addTo(map);

        const icon = L.icon({
            iconUrl: next.img,
            iconSize: [50, 50],
            iconAnchor: [25, 50]
        });

        const marker = L.marker(next.coords, { icon }).addTo(map);

        marker.bindPopup(`
            <b>${next.name}</b><br>
            <img src="${next.img}" width="150"><br>
            <button class="openScannerBtn">Сканировать QR</button>
        `);

        L.marker(currentPosition).addTo(map).bindPopup("Отсюда");

        map.fitBounds([currentPosition, next.coords]);

        // обновляем текущую позицию маршрута
        currentPosition = next.coords;
    }

    // =============================
    // СКАНЕР
    // =============================
    async function openScanner() {
        document.getElementById("scannerModal").style.display = "flex";

        if (scanner) {
            try {
                await scanner.stop();
                await scanner.clear();
            } catch (e) { }
        }

        scanner = new Html5Qrcode("scanner");

        try {
            const cameras = await Html5Qrcode.getCameras();

            if (!cameras || cameras.length === 0) {
                alert("❌ Камера не найдена");
                return;
            }

            await scanner.start(
                { facingMode: "environment" },
                {
                    fps: 10,
                    qrbox: { width: 250, height: 250 }
                },
                onScanSuccess,
                () => { }
            );

        } catch (err) {
            console.error(err);
            alert("❌ Ошибка запуска камеры: " + err);
        }
    }

    async function stopScanner() {
        if (scanner) {
            try {
                await scanner.stop();
                await scanner.clear();
            } catch (e) { }
        }
        document.getElementById("scannerModal").style.display = "none";
    }

    // =============================
    // ОБРАБОТКА QR
    // =============================
    async function onScanSuccess(text) {

        await stopScanner();

        let data;

        try {
            data = JSON.parse(text);
        } catch {
            alert("❌ Это не QR грибного квеста");
            return;
        }

        if (data.type !== "mushroom" || !data.id) {
            alert("❌ Неверный QR код");
            return;
        }

        const expected = mushrooms[currentTarget - 1];

        if (data.id !== expected.id) {
            alert("⚠️ Это не тот гриб!\nНайдите: " + expected.name);
            return;
        }

        // правильный гриб
        mushroomExp[currentTarget - 1] = 30;
        alert("🍄 Найден правильный гриб!\n+30 XP");

        currentTarget++;

        if (currentTarget > mushrooms.length) {
            showFinalModal();
            return;
        }

        buildRouteToNext();
    }

    // =============================
    // ФИНАЛ
    // =============================
    function showFinalModal() {
        document.getElementById("finalModal").style.display = "flex";
    }

    document.getElementById("rewardBtn").onclick = () => {
        window.location.href = "RyazanTrip.App/src/Views/BonusesView.cs";
    };

});