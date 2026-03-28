function showRegisterForm() {
    const loginForm = document.getElementById('login-form');
    const registerForm = document.getElementById('register-form');
    loginForm.classList.remove('active');
    setTimeout(() => {
        loginForm.style.display = 'none';
        registerForm.style.display = 'block';
        setTimeout(() => {
            registerForm.classList.add('active');
        }, 10);
    }, 300);
}
function showLoginForm() {
    const loginForm = document.getElementById('login-form');
    const registerForm = document.getElementById('register-form');
    registerForm.classList.remove('active');
    setTimeout(() => {
        registerForm.style.display = 'none';
        loginForm.style.display = 'block';
        setTimeout(() => {
            loginForm.classList.add('active');
        }, 10);
    }, 300);
}

const loginFormView = document.getElementById("login-form");
const registrationFormView = document.getElementById("register-form");
const loginForm = loginFormView.getElementsByTagName("form")[0];
const registrationForm = loginFormView.getElementsByTagName("form")[0];
loginForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    try {
        const formData = new FormData(loginForm);
        const jsonData = Object.fromEntries(formData);
        const response = await fetch('/api/login', {
            method: "post",
            credentials: "include",
            headers: {
                'Content-Type': "application/json",
            },
            body: JSON.stringify(jsonData)
        })
        if (!response.ok) throw new Error(`Ошибка: ${response.status}`)
        const result = await response.json();
        if (result["success"] === true) {
            document.location = '/profile'
        }
    }  catch (e) {
        console.error('Ошибка: ', e)
    }
})

registrationForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    try {
        const formData = new FormData(registrationForm);
        const jsonData = Object.fromEntries(formData);
        const response = await fetch('/api/registration', {
            method: "post",
            credentials: "include",
            headers: {
                'Content-Type': "application/json",
            },
            body: JSON.stringify(jsonData)
        })
        if (!response.ok) throw new Error(`Ошибка: ${response.status}`)
        const result = await response.json();
        if (result["success"] === true) {
            document.location = '/profile'
        } 
    }
    catch (e) {
        console.error('Ошибка: ', e)
    }
})
    
// Yandex auth
document.getElementById("yandex_submit").addEventListener("click", async (e) => {
    try {
        const formData = new FormData(registrationFormView);
        const jsonData = Object.fromEntries(formData);
        const response = await fetch('/api/login-yandex', {
            method: "post",
            credentials: "include",
            headers: {
                'Content-Type': "application/json",
            },
            body: JSON.stringify(jsonData)
        })
        if (!response.ok) throw new Error(`Ошибка: ${response.status}`)
        const result = await response.json();
        if (result["success"] === true) {
            document.location = '/profile'
        }
    }
    catch (e) {
        console.error('Ошибка: ', e)
    }
})

document.getElementById("show_button_register").addEventListener('click', (x) => {
    showRegisterForm();
})

document.getElementById("show_button_login").addEventListener('click', (x) => {
    showLoginForm();
})


document.addEventListener('DOMContentLoaded', x=> {
    const loginForm = document.getElementById('login-form');
    if (loginForm) {
        loginForm.classList.add('active');
    }
    console.log('Auth module loaded successfully');
})