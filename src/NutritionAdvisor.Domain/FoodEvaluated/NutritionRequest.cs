using System.ComponentModel.DataAnnotations;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Domain.Persona;

namespace NutritionAdvisor.Domain.FoodEvaluated
{
    public class NutritionRequest
    {
        public Goal Goal { get; set; }
        public Person Person { get; set; }
        public IEnumerable<Food> Food { get; set; }
    }
}
