using System.ComponentModel.DataAnnotations;

namespace NutritionAdvisor.Domain.FoodUnevaluated
{
    public class Food
    {
        public string? Name { get; set; }
        public float AmountG { get; set; }
    }
}