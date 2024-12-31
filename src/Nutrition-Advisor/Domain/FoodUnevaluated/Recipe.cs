namespace Nutrition_Advisor.Domain.Food
{
    public class Recipe
    {
        public string Name { get; set; }
        public IEnumerable<Food> Ingredients { get; set; }
    }
}
