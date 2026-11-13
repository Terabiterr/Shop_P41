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
    return data.value; // Зазвичай дані повертають, щоб використовувати далі в коді

  } catch (error) {
    console.error('Помилка запиту:', error);
  }
}

async function add_products(products) {
  const div_products = document.getElementById("products")
  products.forEach(product => {
    const card = `
      <section class="card" style="max-width: 320px">
      <img src="./img/${product.productImages.length > 0 ? product.productImages[0].imageUrl : "default.jpg"}" class="card-img-top" alt="...">
      <div class="card-body">
        <h4 class="card-title">${product.name}</h4>
        <p class="card-text">${product.description}</p>
        <p class="card-text">${product.price}</p>
        <p class="card-text">${product.quantity}</p>
        <a href="#" class="btn-solid theme-success">Buy</a>
      </div>
    </section>
    `
    div_products.innerHTML += card
  });
}

// Головна асинхронна функція для запуску коду
async function main() {
    const token = await get_token()
    const products = await get_products(token)
    console.log(products)
    await add_products(products)
}

main();