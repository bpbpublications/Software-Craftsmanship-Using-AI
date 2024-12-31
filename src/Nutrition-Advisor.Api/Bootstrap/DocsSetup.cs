using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace NutritionAdvisor.Api.Bootstrap
{
    public static class DocsSetup
    {
        public static IServiceCollection AddDocsServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
                .AddMvc()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nutrition-Advisor.Api", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Nutrition-Advisor.Api", Version = "v2" });
            });

            services.AddSwaggerExamples();
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            return services;
        }
    
    }
}
