const createTourForm = document.getElementById("createTourForm");
createTourForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    try {
        const formData = new FormData(createTourForm);
        const jsonData = Object.fromEntries(formData);
        const response = await fetch('/api/tours/create', {
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
        } else {
            alert('Ответ пришел отрицательным');
        }
    }  catch (e) {
        console.error('Ошибка: ', e)
    }
})