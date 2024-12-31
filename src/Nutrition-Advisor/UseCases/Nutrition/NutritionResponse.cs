using Nutrition_Advisor.Domain.Intake;

public class NutritionResponse
{
    public string Message { get; set; }
    public IEnumerable<string> RecommendedFood { get; set; }
    public DietComparison DietComparison { get; set; }
}
