using NutritionAdvisor.Domain.FoodUnevaluated;

namespace NutritionAdvisor.Domain.FoodEvaluated
{
    public class DailyFoodIntake : FoodProperties
    {
        public DailyFoodIntake()
        {
        }

        public DailyFoodIntake(IEnumerable<FoodIntake> foodConsumed)
        {
            Name = "Actually Consumed";
            Sugar = foodConsumed.Sum(f => f.Food.Sugar * f.AmountG / 100);
            Fat = foodConsumed.Sum(f => f.Food.Fat * f.AmountG / 100);
            Protein = foodConsumed.Sum(f => f.Food.Protein * f.AmountG / 100);
            Carbohydrates = foodConsumed.Sum(f => f.Food.Carbohydrates * f.AmountG / 100);
            Kcal = foodConsumed.Sum(f => f.Food.Kcal * f.AmountG / 100);
        }

        public class Recommended
        {
            public float MaxSugar { get; set; }
            public float MaxFat { get; set; }
            public float MaxCarbs { get; set; }
            public float MinProtein { get; set; }
            public float MaxKcal { get; set; }
        }
    }
}