namespace Nutrition_Advisor.Domain.Intake
{
    public class DietComparison
    {
        public DailyFoodIntake Daily { get; set; }
        public DailyFoodIntake.Recommended Recommended { get; set; }
    }
}