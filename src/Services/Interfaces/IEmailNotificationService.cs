namespace Services.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendDailyNotificationsAsync();
    }
}
