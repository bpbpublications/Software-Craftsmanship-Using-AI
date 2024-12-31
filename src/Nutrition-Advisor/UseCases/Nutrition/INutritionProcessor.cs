namespace Nutrition_Advisor.UseCases.Nutrition
{
    public interface INutritionProcessor
    {
        Task<NutritionResponse> Process(NutritionRequest request);
    }
}
