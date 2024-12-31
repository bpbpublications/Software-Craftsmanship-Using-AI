using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;

namespace NutritionAdvisor.UseCases.Nutrition
{
    public interface INutritionProcessorCustom : INutritionProcessor { }

    public class NutritionProcessor : INutritionProcessorCustom
    {
        private readonly IFoodProductsProvider _foodProductsProvider;
        private readonly INutritionResponseBuilder _responseBuilder;
        private readonly IFoodEvaluator _foodEvaluator;

        public NutritionProcessor(
            IFoodProductsProvider foodProductsProvider,
            INutritionResponseBuilder responseBuilder,
            IFoodEvaluator foodEvaluator)
        {
            _foodProductsProvider = foodProductsProvider;
            _responseBuilder = responseBuilder;
            _foodEvaluator = foodEvaluator;
        }

        public async Task<NutritionResponse> Process(NutritionRequest request)
        {
            var foodProductsWithNutritionValue = await _foodProductsProvider.GetFoodProductsAsync(request.Food.Select(f => f.Name));
            var dietaryComparison = _foodEvaluator.CompareFoodConsumedToGoal(request, foodProductsWithNutritionValue.Values);

            var response = _responseBuilder.Build(request.Goal, dietaryComparison);
            return response;
        }
    }
}
