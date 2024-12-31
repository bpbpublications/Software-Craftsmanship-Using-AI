using Nutrition_Advisor.Domain.Food;
using Nutrition_Advisor.UseCases.Nutrition;

namespace Nutrition_Advisor.Domain.Intake
{

    public interface IFoodEvaluator
    {
        DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties);
    }

    public class FoodEvaluator : IFoodEvaluator
    {
        private readonly IRecommendedKcalCalculator _recommendedDailyKcalCalculator;
        private readonly IRecommendedDailyIntakeCalculator _recommendedDailyIntakeCalculator;

        public FoodEvaluator(IRecommendedDailyIntakeCalculator recommendedDailyIntakeCalculator, IRecommendedKcalCalculator calculator)
        {
            _recommendedDailyIntakeCalculator = recommendedDailyIntakeCalculator;
            _recommendedDailyKcalCalculator = calculator;
        }

        public DietComparison CompareFoodConsumedToGoal(NutritionRequest request, IEnumerable<FoodProperties> foodProperties)
        {
            var food = foodProperties.Join(request.Food, fp => fp.Name, f => f.Name, (fp, f) => new FoodIntake { Food = fp, AmountG = f.AmountG });
            var daily = new DailyFoodIntake(food);

            var recommendedKcalIntake = _recommendedDailyKcalCalculator.CalculateRecommendedKcalIntake(request.Person, request.Goal);
            var recommended = new DailyFoodIntake.Recommended
            {
                MaxSugar = _recommendedDailyIntakeCalculator.MaxSugar(request.Person.Gender),
                MaxFat = _recommendedDailyIntakeCalculator.MaxFat(recommendedKcalIntake),
                MaxCarbs = _recommendedDailyIntakeCalculator.MaxCarbs(recommendedKcalIntake),
                MinProtein = _recommendedDailyIntakeCalculator.MinProtein(request.Person.Weight, 1.5f),
                MaxKcal = recommendedKcalIntake
            };

            return new DietComparison
            {
                Daily = daily,
                Recommended = recommended
            };
        }
    }
}