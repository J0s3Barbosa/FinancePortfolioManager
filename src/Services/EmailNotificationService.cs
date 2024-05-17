using Services.Interfaces;

namespace DataAccess.Repositories
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IProductService _productService;
        private readonly IEmailSender _emailSender;

        public EmailNotificationService(IProductService productService, IEmailSender emailSender)
        {
            _productService = productService;
            _emailSender = emailSender;
        }

        public async Task SendDailyNotificationsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            var upcomingExpirations = products.Where(p => p.ExpiryDate <= DateTime.UtcNow.AddDays(7)).ToList();

            foreach (var product in upcomingExpirations)
            {
                await _emailSender.SendEmailAsync("admin@company.com", "Upcoming Product Expiration",
                    $"The product {product.Name} is expiring on {product.ExpiryDate:yyyy-MM-dd}.");
            }
        }
    }

}
