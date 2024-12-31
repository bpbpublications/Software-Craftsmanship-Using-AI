using External = NutritionAdvisor.Api.Dtos;
using Internal = NutritionAdvisor.Domain;

namespace NutritionAdvisor.Api.Mappers
{
    public interface INutritionRequestMapper
    {
        Internal.FoodEvaluated.NutritionRequest Map(External.NutritionRequest request);
    }

    public class NutritionRequestMapper : INutritionRequestMapper
    {
        public Internal.FoodEvaluated.NutritionRequest Map(External.NutritionRequest request)
        {
            return new Internal.FoodEvaluated.NutritionRequest
            {
                Goal = GoalFromString(request.Goal),
                Person = Map(request.Person),
                Food = request.Food.Select(Map).ToArray()
            };
        }

        private Internal.Persona.Goal GoalFromString(string goal)
        {
            return goal switch
            {
                "Lose Weight" => Internal.Persona.Goal.LoseWeight,
                "Gain Weight" => Internal.Persona.Goal.GainWeight,
                "Become Fit" => Internal.Persona.Goal.BecomeFit,
                _ => throw new Exception("Unknown goal type"),
            };
        }

        private Internal.FoodUnevaluated.Food Map(External.Food food)
        {
            return new Internal.FoodUnevaluated.Food
            {
                Name = food.Name,
                AmountG = food.AmountG,
            };
        }

        private Internal.Persona.Person Map(External.Person person)
        {
            return new Internal.Persona.Person
            {
                Age = person.Age,
                ActivityLevel = person.ActivityLevel,
                Gender = person.Gender,
                Weight = person.Weight,
                Height = person.Height
            };
        }
    }
}
