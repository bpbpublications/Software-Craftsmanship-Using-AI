using System.ComponentModel.DataAnnotations;

namespace NutritionAdvisor.Api.Dtos
{
    public class Food
    {
        [MinLength(1)]
        public string? Name { get; set; }
        [Range(0.01, 1000)]
        public float AmountG { get; set; }
    }
}
