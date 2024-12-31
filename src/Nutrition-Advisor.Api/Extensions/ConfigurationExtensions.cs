namespace NutritionAdvisor.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetPlaceholderedValueOf(this IConfiguration configuration, string property)
        {
            var value = configuration[property];
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException($"{property} is required");
            }

            // validate value doesn't start with #{ and end with }#
            if (value.StartsWith("#{") && value.EndsWith("}#"))
            {
                throw new InvalidOperationException($"{property} is a placeholder and should be resolved");
            }

            return value;
        }
    }
}
