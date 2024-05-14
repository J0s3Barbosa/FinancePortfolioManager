using System;
using System.Threading.Tasks;
using DataAccess.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace InvestmentPortfolioEmailNotification
{
    public class TimerTriggerFunction
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public TimerTriggerFunction(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        [FunctionName("SendEmailNotifications")]
        public async Task Run([TimerTrigger("0 0 9 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"SendEmailNotifications function executed at: {DateTime.Now}");
            await _emailNotificationService.SendDailyNotificationsAsync();
        }

    }
}
