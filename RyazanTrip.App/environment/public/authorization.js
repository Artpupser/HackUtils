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