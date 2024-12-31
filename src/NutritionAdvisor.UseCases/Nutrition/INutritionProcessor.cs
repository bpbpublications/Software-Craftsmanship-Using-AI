using NutritionAdvisor.Domain.FoodEvaluated;

namespace NutritionAdvisor.UseCases.Nutrition
{
    public interface INutritionProcessor
    {
        Task<NutritionResponse> Process(NutritionRequest request);
    }
}
