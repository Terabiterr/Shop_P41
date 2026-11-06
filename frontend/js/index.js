const server_url = `http://localhost:5089/`;

// Очистили функцію від суміші async та .then(), зробивши її повністю на async/await
async function get_token() {
    try {
        const res = await fetch(server_url + 'api/apiuser/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "email": "admin@gmail.com",
                "password": "0000"
            })
        });

        if (!res.ok) throw new Error(`Status: ${res.status}`);

        const data = await res.json();
        return data.token;
    } catch (err) {
        console.error("Помилка:", err);
    }
}

// Головна асинхронна функція для запуску коду
async function main() {
    const token = await get_token()
    await get_products(token)
    console.log(token);
}

main();

async function get_products(token) {
  try {
    const response = await fetch(
        server_url + 'api/apiproducts', 
        {
      method: 'GET', // Для GET-запиту зазвичай headers та body налаштовуються, але якщо сервер вимагає — залишаємо
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });

    // Перевіряємо, чи запит успішний (статус у діапазоні 200-299)
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    const data = await response.json();
    console.log(data);
    return data; // Зазвичай дані повертають, щоб використовувати далі в коді

  } catch (error) {
    console.error('Помилка запиту:', error);
  }
}
