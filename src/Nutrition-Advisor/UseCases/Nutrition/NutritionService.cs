using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Notification;
using Nutrition_Advisor.UseCases.Notification;

namespace Nutrition_Advisor.UseCases.Nutrition
{
    public interface INutritionService
    {
        Task<NutritionResponse> GetNutritionResponse(NutritionRequest request);
    }

    public class NutritionService : INutritionService
    {
        private readonly ILogger<NutritionService> _logger;
        private readonly INotificationsFacade _notifier;
        private readonly NotificationsConfig _config;
        private readonly INutritionProcessor _processor;

        public NutritionService(
                       ILogger<NutritionService> logger,
                       INotificationsFacade notifier,
                       NotificationsConfig config,
                       INutritionProcessor processor)
        {
            _logger = logger;
            _notifier = notifier;
            _config = config;
            _processor = processor;
        }

        public async Task<NutritionResponse> GetNutritionResponse(NutritionRequest request)
        {
            NutritionResponse response = await _processor.Process(request);

            SendNotification(response);

            _logger.LogInformation($"Nutrition response generated for goal {request.Goal.Name}.");

            return response;
        }

        private void SendNotification(NutritionResponse response)
        {
            if (_config.IsEmailEnabled)
            {
                _notifier.SendEmailNotificationAsync(response.Message, _config.Email);
            }
            else
            {
                _logger.LogInformation("Email notifications are disabled.");
            }

            if (_config.IsSmsEnabled)
            {
                _notifier.SendSmsNotificationAsync(response.Message, _config.Phone);
            }
            else
            {
                _logger.LogInformation("SMS notifications are disabled.");
            }
        }
    }
}
