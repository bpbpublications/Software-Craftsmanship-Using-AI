using NutritionAdvisor.Domain.FoodEvaluated;
using NutritionAdvisor.Domain.FoodUnevaluated;
using NutritionAdvisor.Infrastructure.FoodApi;
using NutritionAdvisor.Infrastructure.Gpt;
using NutritionAdvisor.Infrastructure.Notificaitons;
using NutritionAdvisor.Infrastructure.Notificaitons.EmailApi;
using NutritionAdvisor.Infrastructure.Notificaitons.SmsApi;
using NutritionAdvisor.UseCases.Notification;
using NutritionAdvisor.UseCases.Nutrition;

namespace NutritionAdvisor.Api.Bootstrap
{
    public static class DomainSetup
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<INutritionResponseBuilder, NutritionResponseBuilder>();
            services.AddScoped<INotificationsFacade, NotificationsFacade>();
            services.AddScoped<NotificationsConfig>();
            services.AddScoped<IFoodProductsProvider, FoodProductsProvider>();
            services.AddScoped<IFoodEvaluator, FoodEvaluator>();
            services.AddScoped<IRecommendedDailyIntakeCalculator, RecommendedDailyIntakeCalculator>();
            services.AddScoped<IRecommendedKcalCalculator, RecommendedKcalCalculator>();
            services.AddScoped<IEmailAdapter, EmailAPIAdapter>();
            services.AddScoped<ISmsAdapter, SmsAPIAdapter>();
            services.AddScoped<IFoodApiAdapter, FoodApiAdapter>();
            services.AddScoped<INutritionProcessorCustom, NutritionProcessor>();
            services.AddScoped<INutritionProcessorGpt, NutritionProcessorChatGpt>();
            services.AddScoped<INutritionServiceV1, NutritionServiceV1>();
            services.AddScoped<INutritionServiceV2, NutritionServiceV2>();

            return services;
        }
    }
}
