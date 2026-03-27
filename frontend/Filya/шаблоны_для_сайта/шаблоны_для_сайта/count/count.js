document.querySelectorAll('.counter').forEach(counter => {
    let count = 0;
    const countEl = counter.querySelector('.count');
    const increaseBtn = counter.querySelector('.increase');
    const decreaseBtn = counter.querySelector('.decrease');

    increaseBtn.addEventListener('click', () => {
        count++;
        countEl.textContent = count;
    });

    decreaseBtn.addEventListener('click', () => {
        count--;
        countEl.textContent = count;
    });
});