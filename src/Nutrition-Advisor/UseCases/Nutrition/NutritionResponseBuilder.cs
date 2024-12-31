using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Intake;
using Nutrition_Advisor.Domain.Person;
using System.Text;

namespace Nutrition_Advisor.UseCases.Nutrition
{
    public interface INutritionResponseBuilder
    {
        NutritionResponse Build(Goal goal, DietComparison dietaryComparison);
    }

    public class NutritionResponseBuilder : INutritionResponseBuilder
    {
        public NutritionResponse Build(Goal goal, DietComparison dietaryComparison)
        {
            var message = FormatResponse(goal.FoodRecommendations, dietaryComparison);

            return new NutritionResponse
            {
                DietComparison = dietaryComparison,
                Message = message,
                RecommendedFood = goal.FoodRecommendations
            };
        }

        private string FormatResponse(IEnumerable<string> foodRecommendations, DietComparison diet)
        {
            var formattedResponse = new StringBuilder();

            // Header
            formattedResponse.AppendLine("| Nutrient | Consumed       | Recommendation      | Difference       |");
            formattedResponse.AppendLine("|----------|----------------|---------------------|------------------|");

            // Sugar
            formattedResponse.AppendLine($"| Sugar    | {diet.Daily.Sugar,10:F2}g    | {diet.Recommended.MaxSugar,15:F2}g    | {diet.Daily.Sugar - diet.Recommended.MaxSugar,12:F2}g    |");

            // Fat
            formattedResponse.AppendLine($"| Fat      | {diet.Daily.Fat,10:F2}g    | {diet.Recommended.MaxFat,15:F2}g    | {diet.Daily.Fat - diet.Recommended.MaxFat,12:F2}g    |");

            // Protein
            formattedResponse.AppendLine($"| Protein  | {diet.Daily.Protein,10:F2}g    | {diet.Recommended.MinProtein,15:F2}g    | {diet.Daily.Protein - diet.Recommended.MinProtein,12:F2}g    |");

            // Carbs
            formattedResponse.AppendLine($"| Carbs    | {diet.Daily.Carbohydrates,10:F2}g    | {diet.Recommended.MaxCarbs,15:F2}g    | {diet.Daily.Carbohydrates - diet.Recommended.MaxCarbs,12:F2}g    |");

            // Calories
            formattedResponse.AppendLine($"| Calories | {diet.Daily.Kcal,10:F2}kcal | {diet.Recommended.MaxKcal,15:F2}kcal | {diet.Daily.Kcal - diet.Recommended.MaxKcal,12:F2}kcal |");

            // Food Recommendations
            formattedResponse.AppendLine("\nFood Recommendations:");
            foreach (var recommendation in foodRecommendations)
            {
                formattedResponse.AppendLine($"- {recommendation}");
            }

            return formattedResponse.ToString();
        }
    }
}