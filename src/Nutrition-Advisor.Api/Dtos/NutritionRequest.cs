using System.ComponentModel.DataAnnotations;

namespace NutritionAdvisor.Api.Dtos
{
    public class NutritionRequest
    {
        [Required]
        public string Goal { get; set; }
        [Required]
        public Person Person { get; set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<Food> Food { get; set; }
    }
}
