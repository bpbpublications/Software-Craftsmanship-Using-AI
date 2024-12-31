using NutritionAdvisor.Api.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

ServicesSetup.AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

// swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nutrition-Advisor.Api v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Nutrition-Advisor.Api v2");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add health checks
app.MapHealthChecks("/health");

// middleware to log every request. Use an inject logger to do logging
app.Use(async (context, next) =>
{
    // resolve logger from context
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    // log request
    logger.LogInformation($"{context.Request.Path}: sending");
    await next.Invoke();
    // log response with request path and status code
    logger.LogInformation($"{context.Request.Path} completed: {context.Response.StatusCode}");
});

app.Run();

public partial class Program { }