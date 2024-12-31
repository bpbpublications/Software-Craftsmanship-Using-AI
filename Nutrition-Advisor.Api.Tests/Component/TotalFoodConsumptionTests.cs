using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;
using Dto = NutritionAdvisor.Api.Dtos;
using System.Text;
using static NutritionAdvisor.Tests.Api.Dummy.DummyValueGenerator;

namespace NutritionAdvisor.Api.Tests.Component
{
    public class TotalFoodConsumptionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        private readonly Mock<IFoodApiAdapter> mockFoodApiAdapter;

        public TotalFoodConsumptionTests(WebApplicationFactory<Program> factory)
        {
            mockFoodApiAdapter = new Mock<IFoodApiAdapter>();
            client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Replace the registered IFoodApiAdapter with the mock
                    services.AddScoped<IFoodApiAdapter>(_ => mockFoodApiAdapter.Object);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task DailyFoodIntake_WhenSingleFood_ReturnsThatFood()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var foodInRequest = new[] { new Dto.Food { Name = "Gyros", AmountG = 200 } };
            var requestBody = BuildFoodRequestBody(foodInRequest);
            var request = BuildFoodRequest(requestBody);

            // Act
            var response = await client.SendAsync(request);

            // Assert
            var requestedFoods = new[] { requestedFood };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequest);
        }

        private HttpRequestMessage BuildFoodRequest(string requestBody)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/nutrition")
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json"),
                Headers = { { "X-API-KEY", "444A19BE-FCFD-4C0E-9FCB-3A833F13C6B1" } }
            };

            return request;
        }

        [Fact]
        public async Task DailyFoodIntake_WhenMultipleFoods_ReturnsSumOfTheirValues()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var requestedFood2 = SetupFoodReturned("French Fries");
            var foodInRequest = new[] { new Dto.Food { Name = "Gyros", AmountG = 200 }, new Dto.Food { Name = "French Fries", AmountG = 200 } };
            var requestBody = BuildFoodRequestBody(foodInRequest);
            var request = BuildFoodRequest(requestBody);

            // Act
            var response = await client.SendAsync(request);

            // Assert
            var requestedFoods = new[] { requestedFood, requestedFood2 };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequest);
        }

        [Fact]
        public async Task DailyFoodIntake_WhenRecipe_ReturnsSumOfRecipeIngredients()
        {
            // Arrange
            var requestedFood = SetupFoodReturned("Gyros");
            var requestedFood2 = SetupFoodReturned("French Fries");
            SetupRecipeReturned("Gyros with French Fries", "Gyros", "French Fries");
            var foodInRequest = new[] { new Dto.Food { Name = "Gyros with French Fries", AmountG = 100 } };
            var requestBody = BuildFoodRequestBody(new Dto.Food() { Name = "Gyros with French Fries", AmountG = 100 });
            var request = BuildFoodRequest(requestBody);

            // Act
            var response = await client.SendAsync(request);

            // Assert
            var requestedFoods = new[] { requestedFood, requestedFood2 };
            var foodInRequestActual = new[] { 
                // When setting up food we hardcode every ingredient in the recipe to be 100g for simplicity.
                // Prone to bugs, but this should have been unit tested at lower levels in any case.
                new Dto.Food { Name = "Gyros", AmountG = 100 },
                new Dto.Food { Name = "French Fries", AmountG = 100 } 
            };
            await AssertResponseEqualsExpectedFoodIntake(response, requestedFoods, foodInRequestActual);
        }

        private string BuildFoodRequestBody(params Dto.Food[] foods)
        {
            var requestData = new Dto.NutritionRequest
            {
                Goal = "Become Fit",
                Person = Any<Dto.Person>(),
                Food = foods
            };

            return JsonConvert.SerializeObject(requestData);
        }

        private FoodProperties SetupFoodReturned(string name)
        {
            var requestedFood = Any<FoodProperties>();
            requestedFood.Name = name;
            mockFoodApiAdapter
                .Setup(f => f.GetFoodPropertyAsync(requestedFood.Name))
                .ReturnsAsync(requestedFood);
            return requestedFood;
        }

        private async Task AssertResponseEqualsExpectedFoodIntake(HttpResponseMessage response, FoodProperties[] foodProperties, Dto.Food[] foodInRequest)
        {
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var nutritionResponse = JsonConvert.DeserializeObject<NutritionResponse>(responseContent);

            var ingredientsWithAmounts = foodInRequest.Zip(foodProperties, (fa, fp) => new FoodIntake() { Food = fp, AmountG = fa.AmountG });
            var expectedDailyFoodIntake = new DailyFoodIntake(ingredientsWithAmounts);

            expectedDailyFoodIntake.Should().BeEquivalentTo(nutritionResponse.DietComparison.Daily);
        }

        private Recipe SetupRecipeReturned(string recipeName, params string[] ingredients)
        {
            var recipe = new Recipe
            {
                Name = recipeName,
                Ingredients = ingredients.Select(i => new Food { Name = i, AmountG = 100 })
            };
            mockFoodApiAdapter
                .Setup(f => f.GetRecipeAsync(recipeName))
                .ReturnsAsync(recipe);

            return recipe;
        }
    }
}