namespace NutritionAdvisor.UseCases.Notification
{
    public interface INotificationsFacade
    {
        ValueTask SendEmailNotificationAsync(string body, string recipient);
        ValueTask SendSmsNotificationAsync(string body, string recipient);
    }

}
