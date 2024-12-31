// TODO: Uncomment the code below and complete the tests

//using Microsoft.Extensions.Logging;
//using Moq;
//using NutritionAdvisor.Domain.FoodEvaluated;
//using NutritionAdvisor.Domain.FoodUnevaluated;
//using NutritionAdvisor.Domain.Persona;
//using NutritionAdvisor.UseCases.Notification;
//using NutritionAdvisor.UseCases.Nutrition;
//using static NutritionAdvisor.Tests.Dummy.DummyValueGenerator;

//namespace NutritionAdvisor.Tests
//{
//    public class NutritionServiceTests
//    {
//        private const string Phone = "123456789";
//        private const string Email = "example@example.com";

//        private readonly NutritionService _nutritionService;
//        private readonly Mock<ILogger<NutritionService>> _loggerMock = new Mock<ILogger<NutritionService>>();
//        private readonly Mock<INotificationsFacade> _notifierMock = new Mock<INotificationsFacade>();
//        private readonly Mock<INutritionResponseBuilder> _responseBuilderMock = new Mock<INutritionResponseBuilder>();
//        private readonly Mock<IFoodProductsProvider> _foodProductsProviderMock = new Mock<IFoodProductsProvider>();
//        private readonly Mock<IFoodEvaluator> _foodEvaluatorMock = new Mock<IFoodEvaluator>();
//        private readonly NotificationsConfig _config = new NotificationsConfig();


//        public NutritionServiceTests()
//        {
//            _nutritionService = new NutritionService(
//                _loggerMock.Object,
//                _responseBuilderMock.Object,
//                _notifierMock.Object,
//                _config,
//                _foodProductsProviderMock.Object,
//                _foodEvaluatorMock.Object);
//        }

//        [Fact]
//        public async Task GetNutritionResponse_ShouldGenerateResponse()
//        {
//            // Arrange
//            var request = new NutritionRequest
//            {
//                Goal = Goal.BecomeFit,
//                Food = new[]
//                {
//                    new Food { Name = "any" }
//                }
//            };
//            var expectedFoodProductsWithNutritionValue = new Dictionary<string, FoodProperties>();
//            GetProductsUsesAndReturns(request.Food, expectedFoodProductsWithNutritionValue);

//            var expectedDietComparison = new DietComparison();
//            CompareFoodConsumedUsesAndReturns(request, expectedFoodProductsWithNutritionValue, expectedDietComparison);

//            var expectedResponse = new NutritionResponse();
//            BuildResponseUsesAndReturns(request.Goal, expectedDietComparison, expectedResponse);

//            // Act
//            var response = await _nutritionService.GetNutritionResponse(request);

//            // Assert
//            Assert.Equal(expectedResponse, response);
//        }

//        [Theory]
//        [InlineData(Phone, Email, true, true)]
//        [InlineData(Phone, null, true, false)]
//        [InlineData(null, Email, false, true)]
//        [InlineData(null, null, false, false)]
//        public async Task GetNutritionResponse_WhenPhoneOrEmailSet_ShouldSendNotification(string phone, string email, bool notifiedPhone, bool notifiedEmail)
//        {
//            // Arrange
//            _config.Phone = phone;
//            _config.Email = email;

//            var anyRequest = Any<NutritionRequest>();
//            GetProductsUsesAndReturns(
//                anyRequest.Food,
//                Any<Dictionary<string, FoodProperties>>());

//            var expectedResponse = Any<NutritionResponse>();
//            BuildResponseUsesAndReturns(
//                anyRequest.Goal,
//                It.IsAny<DietComparison>(),
//                expectedResponse);

//            // Act
//            var response = await _nutritionService.GetNutritionResponse(anyRequest);

//            // Assert
//            VerifyEmailSent(notifiedEmail, response.Message);
//            VerifySmsSent(notifiedPhone, response.Message);
//        }

//        private void GetProductsUsesAndReturns(IEnumerable<Food> expectedFoodUsed, Dictionary<string, FoodProperties> expectedFoodProductsWithNutritionValue)
//        {
//            _foodProductsProviderMock
//                .Setup(provider => provider.GetFoodProductsAsync(It.Is<IEnumerable<string>>(it => it.SequenceEqual(expectedFoodUsed.Select(f => f.Name)))))
//                .ReturnsAsync(expectedFoodProductsWithNutritionValue);
//        }

//        private void CompareFoodConsumedUsesAndReturns(NutritionRequest request, Dictionary<string, FoodProperties> expectedFoodProductsWithNutritionValue, DietComparison expectedDietComparison)
//        {
//            _foodEvaluatorMock
//                .Setup(evaluator => evaluator.CompareFoodConsumedToGoal(request, expectedFoodProductsWithNutritionValue.Values))
//                .Returns(expectedDietComparison);
//        }

//        private void BuildResponseUsesAndReturns(Goal goal, DietComparison expectedDietComparison, NutritionResponse expectedResponse)
//        {
//            _responseBuilderMock
//                .Setup(builder => builder.Build(goal, expectedDietComparison))
//                .Returns(expectedResponse);
//        }

//        private void VerifyEmailSent(bool notifiedEmail, string message)
//        {
//            _notifierMock.Verify(notifier => notifier.SendEmailNotificationAsync(
//                            message, _config.Email), Times.Exactly(Convert.ToInt32(notifiedEmail)));
//        }

//        private void VerifySmsSent(bool notifiedPhone, string message)
//        {
//            _notifierMock.Verify(
//                            notifier => notifier.SendSmsNotificationAsync(message, _config.Phone), Times.Exactly(Convert.ToInt32(notifiedPhone)));
//        }
//    }
//}
