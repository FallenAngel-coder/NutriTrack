using NutriTrack.Models;

namespace NutriTrack.Services
{
    /// <summary>
    /// Сервіс аутентифікації. Інкапсулює логіку хешування паролів та взаємодію з репозиторієм.
    /// </summary>
    public interface IAuthService
    {
        Task<User?> LoginAsync(string username, string password);
        Task<User> RegisterAsync(User newUser, string password);
    }
}