const createTourForm = document.getElementById("createTourForm");
function toBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
        reader.readAsDataURL(file);
    });
}

createTourForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    try {
        const formData = new FormData(createTourForm);
        const jsonData = Object.fromEntries(formData);
        const fileInput = document.getElementById("tourImageInput");
        const file = fileInput.files[0];

        if (file) {
            const base64 =  await toBase64(file);
            jsonData.image = base64; // 👈 кладём base64
        }
        
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

