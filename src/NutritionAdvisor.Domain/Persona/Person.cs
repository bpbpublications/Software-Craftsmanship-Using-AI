using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutritionAdvisor.Domain.Persona
{
    public class Person
    {
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int Age { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
    }
}
