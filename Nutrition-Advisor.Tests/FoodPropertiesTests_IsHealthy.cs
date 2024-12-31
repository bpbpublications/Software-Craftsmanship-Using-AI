using NutritionAdvisor.Domain.FoodUnevaluated;

namespace NutritionAdvisor.Tests
{
    public class FoodPropertiesTests_IsHealthy
    {
        private readonly FoodProperties food = new FoodProperties
        {
            Kcal = 199,
            Protein = 4,
            Carbohydrates = 19,
            Fat = 4,
            Sugar = 4
        };

        [Theory]
        [InlineData(1, 0, 0, 0, 0)]
        [InlineData(0, 1, 0, 0, 0)]
        [InlineData(0, 0, 1, 0, 0)]
        [InlineData(0, 0, 0, 1, 0)]
        [InlineData(0, 0, 0, 0, -1)]
        public void WhenASinglePropertyDifferentThanThreshold_ReturnsFalse(
            float exceedingKcalBy, 
            float exceedingCarbsBy, 
            float exceedingFatBy,
            float exceedingSugarBy,
            float exceedingProteinBy)
        {
            // Arrange
            food.Kcal += exceedingKcalBy;
            food.Carbohydrates += exceedingCarbsBy;
            food.Fat += exceedingFatBy;
            food.Sugar += exceedingSugarBy;
            food.Protein += exceedingProteinBy;

            // Act
            var result = food.IsHealthy();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void WhenInRecommendations_ReturnsTrue()
        {
            // Act
            var result = food.IsHealthy();

            // Assert
            Assert.True(result);
        }
    }
}
