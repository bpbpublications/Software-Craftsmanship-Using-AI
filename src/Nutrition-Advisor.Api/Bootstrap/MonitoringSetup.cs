namespace NutritionAdvisor.Api.Bootstrap
{
    public static class MonitoringSetup
    {
        public static IServiceCollection AddMonitoringService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry();

            return services;
        }
    }
}
