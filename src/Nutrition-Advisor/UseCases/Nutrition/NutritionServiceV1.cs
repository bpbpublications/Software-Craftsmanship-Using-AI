using Microsoft.Extensions.Logging;
using Nutrition_Advisor.Domain.Notification;
using Nutrition_Advisor.UseCases.Notification;

namespace Nutrition_Advisor.UseCases.Nutrition
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
