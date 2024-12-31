using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Domain.Persona;
using NutritionAdvisor.Infrastructure.FoodApi;
using NutritionAdvisor.Infrastructure.Notificaitons;
using NutritionAdvisor.Infrastructure.Notificaitons.EmailApi;
using NutritionAdvisor.Infrastructure.Notificaitons.SmsApi;
using NutritionAdvisor.UseCases.Notification;
using NutritionAdvisor.UseCases.Nutrition;
using Serilog;

var person = new Person()
{
    Gender = Gender.Male,
    Weight = 75,
    Height = 180,
    Age = 30,
    ActivityLevel = ActivityLevel.ModeratelyActive
};

// demo goals
Goal[] goals = { Goal.GainWeight, Goal.LoseWeight, Goal.BecomeFit };

using var serilogLogger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();

var serviceProvider = new ServiceCollection()
    .AddLogging(builder => builder.AddSerilog(serilogLogger))
    .AddSingleton<IRecommendedKcalCalculator, RecommendedKcalCalculator>()
    .AddSingleton<INutritionResponseBuilder, NutritionResponseBuilder>()
    .AddSingleton<INutritionService, NutritionService>()
    .AddSingleton<IEmailAdapter, EmailAPIAdapter>()
    .AddSingleton<ISmsAdapter, SmsAPIAdapter>()
    .AddSingleton<INotificationsFacade, NotificationsFacade>()
    .AddSingleton<NotificationsConfig>()
    .AddSingleton<IFoodApiAdapter, FoodApiAdapter>()
    .AddSingleton<IFoodProductsProvider, FoodProductsProvider>()
    .AddSingleton<IFoodEvaluator, FoodEvaluator>()
    .AddSingleton<IRecommendedDailyIntakeCalculator, RecommendedDailyIntakeCalculator>()
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<NutritionService>>();
var service = serviceProvider.GetRequiredService<INutritionService>();

foreach (var goal in goals)
{
    logger.LogInformation(goal.Name);
    var request = new NutritionRequest { Food = new[] {
        new Food { Name = "Smoothie", AmountG = 500 },
        new Food { Name = "Chocolate", AmountG = 100 }}
    , Goal = goal, Person = person };
    var response = await service.GetNutritionResponse(request);
    logger.LogInformation(response.Message);
}

Console.ReadLine();
