using NutriTrack.Models;

namespace NutriTrack.Services
{
    public interface IFoodLogService
    {
        Task<List<FoodLog>> GetTodayLogsAsync(int userId);
        Task<FoodLog> AddFoodLogAsync(FoodLog log);
        Task DeleteFoodLogAsync(int logId);
        Task UpdateFoodLogAsync(FoodLog log);
    }
}