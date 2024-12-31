using NutritionAdvisor.Api.Mappers;

namespace NutritionAdvisor.Api.Bootstrap
{
    public class ServicesSetup
    {
        public static void AddServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services
                   .AddDomainServices()
                   .AddControllerServices()
                   .AddOpenAiApiServices(configuration)
                   .AddControllerServices()
                   .AddDocsServices()
                   .AddApplicationInsightsTelemetry()
                   .AddHealthChecks();
                   
            services.AddSingleton<INutritionRequestMapper, NutritionRequestMapper>();
        }
    }
}