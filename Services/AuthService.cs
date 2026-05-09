using NutriTrack.Data.Repositories;
using NutriTrack.Models;

namespace NutriTrack.Services
{
    public class AuthService : IAuthService
    {
        private readonly INutriRepository _repository;

        public AuthService(INutriRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _repository.GetUserByUsernameAsync(username);
            if (user == null) return null;

            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? user : null;
        }

        public async Task<User> RegisterAsync(User newUser, string password)
        {
            var existing = await _repository.GetUserByUsernameAsync(newUser.Username);
            if (existing != null)
                throw new InvalidOperationException("Цей логін вже зайнятий!");

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _repository.AddUserAsync(newUser);
            await _repository.SaveChangesAsync();

            return newUser;
        }
    }
}