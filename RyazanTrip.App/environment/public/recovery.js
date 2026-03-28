// Имитация базы данных пользователей
const usersDB = [
    'makarova@example.com',
    'andryushin@example.com',
    'filimonova@example.com',
    'khandina@example.com',
    'test@test.com'
];

let currentEmail = '';
let generatedCode = '';
let resendTimer = null;

/**
 * ШАГ 1: Отправка кода на почту
 */
function sendCode(event) {
    event.preventDefault();
    
    const email = document.getElementById('recovery-email').value.trim().toLowerCase();
    const errorBlock = document.getElementById('email-error');
    
    // Проверка: есть ли почта в базе
    if (!usersDB.includes(email)) {
        errorBlock.style.display = 'block';
        return;
    }
    
    errorBlock.style.display = 'none';
    currentEmail = email;
    
    // Генерация 6-значного кода
    generatedCode = Math.floor(100000 + Math.random() * 900000).toString();
    
    // Имитация отправки (в реальности - запрос на сервер)
    console.log(`Код для ${email}: ${generatedCode}`);
    alert(`Код отправлен на ${email}:\n${generatedCode}\n\n(В реальном приложении код придёт на почту)`);
    
    // Показываем следующий шаг
    showStep('step-code');
    document.getElementById('email-display').textContent = email;
    
    // Запускаем таймер повторной отправки
    startResendTimer();
}

/**
 * ШАГ 2: Проверка кода
 */
function verifyCode(event) {
    event.preventDefault();
    
    const inputCode = document.getElementById('code-input').value.trim();
    const errorBlock = document.getElementById('code-error');
    
    if (inputCode !== generatedCode) {
        errorBlock.style.display = 'block';
        return;
    }
    
    errorBlock.style.display = 'none';
    showStep('step-password');
}

/**
 * ШАГ 3: Сохранение нового пароля
 */
function savePassword(event) {
    event.preventDefault();
    
    const newPass = document.getElementById('new-password').value;
    const confirmPass = document.getElementById('confirm-password').value;
    const errorBlock = document.getElementById('password-error');
    
    if (newPass !== confirmPass) {
        errorBlock.style.display = 'block';
        return;
    }
    
    if (newPass.length < 6) {
        errorBlock.textContent = 'Пароль должен быть не менее 6 символов';
        errorBlock.style.display = 'block';
        return;
    }
    
    // Имитация сохранения на сервере
    console.log(`Пароль для ${currentEmail} изменён на: ${newPass}`);
    
    errorBlock.style.display = 'none';
    showStep('step-success');
}

/**
 * Повторная отправка кода
 */
function resendCode() {
    generatedCode = Math.floor(100000 + Math.random() * 900000).toString();
    console.log(`Новый код для ${currentEmail}: ${generatedCode}`);
    alert(`Новый код: ${generatedCode}`);
    
    startResendTimer();
}

/**
 * Таймер повторной отправки
 */
function startResendTimer() {
    const timerSpan = document.getElementById('resend-timer');
    const resendBtn = document.getElementById('resend-btn');
    let seconds = 60;
    
    resendBtn.disabled = true;
    timerSpan.style.display = 'inline';
    
    clearInterval(resendTimer);
    
    resendTimer = setInterval(() => {
        seconds--;
        timerSpan.textContent = `Повторить через ${seconds} сек`;
        
        if (seconds <= 0) {
            clearInterval(resendTimer);
            timerSpan.style.display = 'none';
            resendBtn.disabled = false;
        }
    }, 1000);
}

/**
 * Навигация между шагами
 */
function showStep(stepId) {
    // Скрываем все шаги
    document.querySelectorAll('.auth-form').forEach(form => {
        form.classList.remove('active');
        form.style.display = 'none';
    });
    
    // Показываем нужный
    const step = document.getElementById(stepId);
    step.style.display = 'block';
    setTimeout(() => step.classList.add('active'), 10);
}

function goToEmail() {
    clearInterval(resendTimer);
    showStep('step-email');
}

// Инициализация
document.addEventListener('DOMContentLoaded', function() {
    console.log('Recovery module loaded');
    
    // Автофокус на поле кода при переходе
    const codeInput = document.getElementById('code-input');
    if (codeInput) {
        codeInput.addEventListener('input', function(e) {
            // Только цифры
            this.value = this.value.replace(/[^0-9]/g, '');
        });
    }
});