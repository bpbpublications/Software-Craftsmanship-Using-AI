using NutritionAdvisor;

namespace NutritionAdvisor.Api.Bootstrap
{
    public static class ControllersSetup
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}
