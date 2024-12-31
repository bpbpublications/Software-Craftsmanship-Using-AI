namespace Nutrition_Advisor.Domain.Food
{
    public interface IFoodApiAdapter
    {
        Task<Recipe> GetRecipeAsync(string foodItem);
        Task<IEnumerable<string>> GetIngredientsAsync(string compositeFood);
        Task<FoodProperties> GetFoodPropertyAsync(string foodItem);
    }
}
