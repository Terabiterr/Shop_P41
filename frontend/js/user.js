const server_url = `http://localhost:5089/`;
const btn_login = document.getElementById("login")
const btn_register = document.getElementById("register")

btn_login.addEventListener("click", async() => {
    const email = document.getElementById('input_email').value
    const password = document.getElementById('input_password').value

    if(email && password) {
        await login(email, password)
    }
    else {
        alert("email or password are empty")
    }
})

btn_register.addEventListener("click", async() => {
    const email = document.getElementById('input_email').value
    const password = document.getElementById('input_password').value

    if(email && password) {
        register(email, password)
    }
    else {
        alert("email or password are empty")
    }
})

async function login(email, password) {
    const token = await get_token(email, password)
    if(token) {
        localStorage.setItem('token', token)
        alert("Loged in successfully!")
    } else {
        alert("Error!")
    }

}

async function register(email, password) {

}

async function get_token(email, password) {
    try {
        const res = await fetch(server_url + 'api/apiuser/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "email": email,
                "password": password
            })
        });

        if (!res.ok) throw new Error(`Status: ${res.status}`);

        const data = await res.json();
        return data.token;
    } catch (err) {
        console.error("Помилка:", err);
    }
}