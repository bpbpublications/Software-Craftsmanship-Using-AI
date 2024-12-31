using Nutrition_Advisor.Infrastructure.Error;

namespace Nutrition_Advisor.Infrastructure.Notificaitons.Email
{
    public class EmailAPI
    {
        public void SendEmail(string body, string recipient)
        {
            ErrorSimulator.RunWithTransientError(() => Console.WriteLine($"Sending email to {recipient} with body:{Environment.NewLine}{body}"));
        }

        public Task SendEmailAsync(string body, string recipient)
        {
            SendEmail(body, recipient);
            return Task.CompletedTask;
        }
    }
}