namespace Nutrition_Advisor.Domain.Notification
{
    public class NotificationsConfig
    {
        public bool IsEmailEnabled => !string.IsNullOrEmpty(Email);
        public bool IsSmsEnabled => !string.IsNullOrEmpty(Phone);

        public string Email { get; set; } = "test@gmail.com";
        public string Phone { get; set; } = "123456789";
    }
}
