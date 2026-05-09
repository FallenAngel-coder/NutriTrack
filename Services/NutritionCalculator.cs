using NutriTrack.Models;

namespace NutriTrack.Services
{
    public static class NutritionCalculator
    {
        public static double GetTotalCalories(IEnumerable<FoodLog> logs) =>
            logs.Sum(l => l.TotalCalories);

        public static double GetTotalProteins(IEnumerable<FoodLog> logs) =>
            logs.Sum(l => l.TotalProteins);

        public static double GetTotalFats(IEnumerable<FoodLog> logs) =>
            logs.Sum(l => l.TotalFats);

        public static double GetTotalCarbs(IEnumerable<FoodLog> logs) =>
            logs.Sum(l => l.TotalCarbs);
    }
}