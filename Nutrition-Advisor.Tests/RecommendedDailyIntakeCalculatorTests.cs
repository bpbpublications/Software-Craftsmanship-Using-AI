using NutritionAdvisor.Domain.Persona;

namespace NutritionAdvisor.Tests
{
    public class RecommendedDailyIntakeCalculatorTests
    {
        private readonly RecommendedDailyIntakeCalculator calculator;

        public RecommendedDailyIntakeCalculatorTests()
        {
            calculator = new RecommendedDailyIntakeCalculator();
        }

        [Theory]
        [InlineData(1000f, 250f)]
        [InlineData(1500f, 375f)]
        public void MaxFat_ShouldReturnExpected(float recommendedKcalIntake, float expected)
        {
            // Act
            var result = calculator.MaxFat(recommendedKcalIntake);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1000f, 500f)]
        [InlineData(2000f, 1000f)]
        public void MaxCarbs_ShouldReturnExpected(float recommendedKcalIntake, float expected)
        {
            // Act
            var result = calculator.MaxCarbs(recommendedKcalIntake);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(100f, 1f, 100f)]
        [InlineData(150f, 0.8f, 120f)]
        public void MinProtein_ShouldReturnExpected(float personWeight, float minProteinPerKg, float expected)
        {
            // Act
            var result = calculator.MinProtein(personWeight, minProteinPerKg);

            // Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(Gender.Male, 38f)]
        [InlineData(Gender.Female, 25f)]
        public void MaxSugar_WhenGenderIsSpecified_ShouldReturnExpectedValue(Gender gender, float expected)
        {
            // Act
            var result = calculator.MaxSugar(gender);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}