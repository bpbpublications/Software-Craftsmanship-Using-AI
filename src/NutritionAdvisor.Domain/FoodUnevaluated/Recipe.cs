namespace NutritionAdvisor.Domain.FoodUnevaluated
{
    public class Recipe
    {
        public string Name { get; set; }
        public IEnumerable<Food> Ingredients { get; set; }
    }
}
