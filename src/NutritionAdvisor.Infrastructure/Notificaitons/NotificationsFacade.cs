using NutritionAdvisor.Infrastructure.Notificaitons.EmailApi;
using NutritionAdvisor.Infrastructure.Notificaitons.SmsApi;
using NutritionAdvisor.UseCases.Notification;
using Polly;

namespace NutritionAdvisor.Infrastructure.Notificaitons
{

    public class NotificationsFacade : INotificationsFacade
    {
        private readonly IEmailAdapter _emailAdapter;
        private readonly ISmsAdapter _smsAdapter;

        private readonly ResiliencePipeline pipeline;

        public NotificationsFacade(IEmailAdapter emailAdapter, ISmsAdapter smsAdapter)
        {
            _emailAdapter = emailAdapter;
            _smsAdapter = smsAdapter;

            pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new Polly.Retry.RetryStrategyOptions { MaxRetryAttempts = 5 })
                .Build();
        }

        public ValueTask SendEmailNotificationAsync(string body, string recipient)
        {
            return pipeline.ExecuteAsync(async token => await _emailAdapter.SendEmailNotificationAsync(body, recipient, token));
        }

        public ValueTask SendSmsNotificationAsync(string body, string recipient)
        {
            return pipeline.ExecuteAsync(async token => await _smsAdapter.SendSmsNotificationAsync(body, recipient, token));
        }
    }

}
