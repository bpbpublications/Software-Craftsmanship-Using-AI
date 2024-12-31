namespace NutritionAdvisor.Domain.FoodEvaluated
{
    public class DietComparison
    {
        public DailyFoodIntake Daily { get; set; }
        public DailyFoodIntake.Recommended Recommended { get; set; }
    }
}