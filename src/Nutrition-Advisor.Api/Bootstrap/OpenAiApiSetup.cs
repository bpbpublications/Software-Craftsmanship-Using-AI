using NutritionAdvisor.Api.Extensions;

namespace NutritionAdvisor.Api.Bootstrap
{
    public static class OpenAiApiSetup
    {
        public static IServiceCollection AddOpenAiApiServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var openApiKey = configuration.GetPlaceholderedValueOf("OpenAi:ApiKey");

            services.AddOpenAi(settings =>
            {
                settings.ApiKey = openApiKey;
            });

            return services;
        }
    }
}
