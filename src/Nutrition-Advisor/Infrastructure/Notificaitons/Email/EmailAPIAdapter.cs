namespace Nutrition_Advisor.Infrastructure.Notificaitons.Email
{
    public interface IEmailAdapter
    {
        Task SendEmailNotificationAsync(string body, string recipient, CancellationToken ct);
    }

    public class EmailAPIAdapter : IEmailAdapter
    {
        private readonly EmailAPI emailApi;

        public EmailAPIAdapter()
        {
            emailApi = new EmailAPI();
        }

        public Task SendEmailNotificationAsync(string body, string recipient, CancellationToken ct)
        {
            return emailApi.SendEmailAsync(body, recipient);
        }
    }

}
