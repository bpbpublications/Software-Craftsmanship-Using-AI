using NutritionAdvisor.Domain.FoodUnevaluated;

namespace NutritionAdvisor.Domain.FoodEvaluated
{
    public class FoodIntake
    {
        public FoodProperties Food { get; set; }
        public float AmountG { get; set; }
    }
}