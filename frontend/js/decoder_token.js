/**
 * Парсить JWT токен, згенерований через ASP.NET JwtSecurityTokenHandler
 * @param {string} storageKey - Ключ у localStorage (за замовчуванням 'token')
 * @returns {object|null}
 */
export function getUserDataFromToken(storageKey = 'token') {
  const token = localStorage.getItem(storageKey);
  if (!token) return null;

  try {
    // 1. Беремо payload (друга частина токена між крапками)
    const base64Url = token.split('.')[1];
    if (!base64Url) return null;

    // 2. Декодуємо Base64Url з підтримкою кирилиці (UTF-8)
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      window
        .atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    const payload = JSON.parse(jsonPayload);

    // 3. Константи ключів, у які ASP.NET перетворює ClaimTypes
    const CLAIM_KEYS = {
      id: 'http://xmlsoap.org',
      name: 'http://xmlsoap.org',
      email: 'http://xmlsoap.org',
      role: 'http://microsoft.com'
    };

    // 4. Обробка ролей:
    // Коли в C# додається 1 Claim(ClaimTypes.Role, ...), у JSON це рядок: "role": "Admin".
    // Коли додається кілька через foreach, бібліотека автоматично робить масив: "role": ["Admin", "User"].
    const rawRoles = payload[CLAIM_KEYS.role];
    const roles = Array.isArray(rawRoles) 
      ? rawRoles 
      : rawRoles ? [rawRoles] : [];

    return {
      id: payload[CLAIM_KEYS.id] || null,
      login: payload[CLAIM_KEYS.name] || null,
      email: payload[CLAIM_KEYS.email] || null,
      roles, // Завжди повертає масив рядків, навіть якщо роль одна
      isExpired: payload.exp ? Date.now() >= payload.exp * 1000 : false
    };
  } catch (error) {
    console.error('Не вдалося розпарсити JWT токен:', error);
    return null;
  }
}
