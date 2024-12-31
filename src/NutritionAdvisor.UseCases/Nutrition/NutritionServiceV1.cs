using Microsoft.Extensions.Logging;
using NutritionAdvisor.UseCases.Notification;

namespace NutritionAdvisor.UseCases.Nutrition
{
    public interface INutritionServiceV1 : INutritionService { }
    
    public class NutritionServiceV1 : NutritionService, INutritionServiceV1
    {
        public NutritionServiceV1(
            ILogger<NutritionService> logger,
            INotificationsFacade notifier,
            NotificationsConfig config,
            INutritionProcessorCustom processor)
            : base(logger, notifier, config, processor)
        {
        }
    }
}
