using Microsoft.Extensions.Logging;
using NutritionAdvisor.UseCases.Notification;

namespace NutritionAdvisor.UseCases.Nutrition
{
    public interface INutritionServiceV2 : INutritionService { }
    
    public class NutritionServiceV2 : NutritionService, INutritionServiceV2
    {
        public NutritionServiceV2(
            ILogger<NutritionService> logger,
            INotificationsFacade notifier,
            NotificationsConfig config,
            INutritionProcessorGpt processor)
            : base(logger, notifier, config, processor)
        {
        }
    }
}
