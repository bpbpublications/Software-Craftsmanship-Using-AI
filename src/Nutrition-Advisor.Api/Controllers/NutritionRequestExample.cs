using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Domain.Persona;
using Swashbuckle.AspNetCore.Filters;
namespace NutritionAdvisor.Api.Controllers
{
    public class NutritionRequestExample : IExamplesProvider<NutritionRequest>
    {
        public NutritionRequest GetExamples()
        {
            return new NutritionRequest
            {
                Goal = Goal.BecomeFit,
                Person = new Person
                {
                    Age = 30,
                    Height = 180,
                    Weight = 80
                },
                Food = new List<Food>
                {
                    new Food
                    {
                        Name = "Apple",
                        AmountG = 200
                    }
                }
            };
        }
    }

    public class NutritionResponseExample : IExamplesProvider<NutritionResponse>
    {
        public NutritionResponse GetExamples()
        {
            return new NutritionResponse
            {
                Message = "You should eat more vegetables",
                RecommendedFood = new List<string>
                {
                    "Carrot",
                    "Broccoli"
                },
                DietComparison = new DietComparison
                {
                    Daily = new DailyFoodIntake
                    {
                        Kcal = 2000,
                        Protein = 100,
                        Fat = 50,
                        Carbohydrates = 300
                    },
                    Recommended = new DailyFoodIntake.Recommended
                    {
                        MaxKcal = 2500,
                        MinProtein = 150,
                        MaxFat = 60,
                        MaxCarbs = 400,
                        MaxSugar = 50
                    }
                }
            };
        }
    }
}