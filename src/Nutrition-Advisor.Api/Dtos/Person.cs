using NutritionAdvisor.Domain.Persona;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NutritionAdvisor.Api.Dtos
{
    public class Person
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; }
        [Range(1, 1000)]
        public float Weight { get; set; }
        [Range(0.1, 3)]
        public float Height { get; set; }
        [Range(1, 200)]
        public int Age { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ActivityLevel ActivityLevel { get; set; }
    }
}
