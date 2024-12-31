using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Person;
using Serilog.Core;

namespace Nutrition_Advisor.Domain.Intake
{
    public interface IRecommendedKcalCalculator
    {
        float CalculateRecommendedKcalIntake(Person person, Goal goal);
    }

    public class RecommendedKcalCalculator : IRecommendedKcalCalculator
    {
        private readonly ILogger<RecommendedKcalCalculator> _logger;

        public RecommendedKcalCalculator(ILogger<RecommendedKcalCalculator> logger)
        {
            _logger = logger;
        }

        public float CalculateRecommendedKcalIntake(Person person, Goal goal)
        {
            var tdee = CalculateTdee(person);
            _logger.LogInformation("CalculateRecommendedKcalIntake completed.");
            return tdee + goal.RecommendedKcalAdjustment;
        }

        private static float CalculateTdee(Person person)
        {
            float bmr = person.Gender == Gender.Male ? CalculateBmrForMen(person.Weight, person.Height, person.Age) : CalculateBmrForWomen(person.Weight, person.Height, person.Age);
            float tdee = CalculateTdee(bmr, person.ActivityLevel);
            return tdee;
        }

        // Harris-Benedict equation for men
        private static float CalculateBmrForMen(float weight, float height, int age)
        {
            return 88.362f + 13.397f * weight + 4.799f * height - 5.677f * age;
        }

        // Harris-Benedict equation for women
        private static float CalculateBmrForWomen(float weight, float height, int age)
        {
            return 447.593f + 9.247f * weight + 3.098f * height - 4.330f * age;
        }

        private static float CalculateTdee(float bmr, ActivityLevel activityLevel)
        {
            float tdee = 0;

            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    tdee = bmr * 1.2f;
                    break;
                case ActivityLevel.LightlyActive:
                    tdee = bmr * 1.375f;
                    break;
                case ActivityLevel.ModeratelyActive:
                    tdee = bmr * 1.55f;
                    break;
                case ActivityLevel.VeryActive:
                    tdee = bmr * 1.725f;
                    break;
                case ActivityLevel.SuperActive:
                    tdee = bmr * 1.9f;
                    break;
            }

            return tdee;
        }
    }
}
