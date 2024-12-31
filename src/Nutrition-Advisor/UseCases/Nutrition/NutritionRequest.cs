using System.ComponentModel.DataAnnotations;
using Nutrition_Advisor.Domain.Food;
using Nutrition_Advisor.Domain.Person;

namespace Nutrition_Advisor.UseCases.Nutrition
{
    public class NutritionRequest
    {
        [Required]
        public Goal Goal { get; set; }
        [Required]
        public Person Person { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<Food> Food { get; set; }
    }
}
