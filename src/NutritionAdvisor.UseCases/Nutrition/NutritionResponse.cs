using NutritionAdvisor.Domain.FoodEvaluated;

public class NutritionResponse
{
    public string Message { get; set; }
    public IEnumerable<string> RecommendedFood { get; set; }
    public DietComparison DietComparison { get; set; }
}
