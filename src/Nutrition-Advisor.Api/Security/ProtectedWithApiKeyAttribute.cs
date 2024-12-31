using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NutritionAdvisor.Api.Extensions;

namespace NutritionAdvisor.Api.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ProtectedWithApiKeyAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context){}

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var potentialApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key");
                return;
            }

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetPlaceholderedValueOf("ApiKey");

            if (!string.Equals(apiKey, potentialApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid API Key");
                return;
            }
        }
    }
}
