namespace NutritionAdvisor.Domain.FoodUnevaluated
{
    public interface IFoodApiAdapter
    {
        Task<Recipe> GetRecipeAsync(string foodItem);
        Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood);
        Task<FoodProperties> GetFoodPropertyAsync(string foodItem);
    }
}
