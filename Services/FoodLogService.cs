using NutriTrack.Data.Repositories;
using NutriTrack.Models;

namespace NutriTrack.Services
{
    public class FoodLogService : IFoodLogService
    {
        private readonly INutriRepository _repository;

        public FoodLogService(INutriRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FoodLog>> GetTodayLogsAsync(int userId)
        {
            var logs = await _repository.GetLogsByDateAsync(DateTime.Now, userId);
            return logs.ToList();
        }

        public async Task<FoodLog> AddFoodLogAsync(FoodLog log)
        {
            await _repository.AddFoodLogAsync(log);
            await _repository.SaveChangesAsync();
            return log;
        }

        public async Task DeleteFoodLogAsync(int logId)
        {
            await _repository.DeleteLogAsync(logId);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateFoodLogAsync(FoodLog log)
        {
            await _repository.SaveChangesAsync();
        }
    }
}