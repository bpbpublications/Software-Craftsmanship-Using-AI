using Microsoft.AspNetCore.Mvc;
using Moq;
using NutritionAdvisor.UseCases.Nutrition;
using NutritionAdvisor.Api.Controllers;
using NutritionAdvisor.Api.Mappers;
using Dto = NutritionAdvisor.Api.Dtos;
using Model = NutritionAdvisor.Domain.FoodEvaluated;

namespace NutritionAdvisor.Api.Tests
{
    public class NutritionControllerTests
    {
        [Fact]
        public async Task GetNutritionResponse_ReturnsOkObjectResult()
        {
            // Arrange
            var nutritionServiceMock = new Mock<INutritionServiceV1>();
            var mapper = new Mock<INutritionRequestMapper>();

            var nutritionController = new NutritionControllerV1(nutritionServiceMock.Object, mapper.Object);
            var dtoRequest = new Dto.NutritionRequest();
            var domainRequest = new Model.NutritionRequest();

            mapper
                .Setup(x => x.Map(dtoRequest))
                .Returns(domainRequest);
            
            // Act
            var result = await nutritionController.GetNutritionResponse(dtoRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
            nutritionServiceMock.Verify(x => x.GetNutritionResponse(domainRequest), Times.Once);
        }
    }
}