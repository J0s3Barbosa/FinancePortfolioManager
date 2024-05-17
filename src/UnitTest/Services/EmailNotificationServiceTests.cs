using DataAccess.Repositories;
using Domain.Models;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    public class EmailNotificationServiceTests
    {
        [Fact]
        public async Task SendDailyNotificationsAsync_Success()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var emailSenderMock = new Mock<IEmailSender>();

            var product1 = new Product { Name = "Product1", ExpiryDate = DateTime.UtcNow.AddDays(5) };
            var product2 = new Product { Name = "Product2", ExpiryDate = DateTime.UtcNow.AddDays(10) };

            var products = new List<Product> { product1, product2 };

            productServiceMock.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(products);

            var emailNotificationService = new EmailNotificationService(productServiceMock.Object, emailSenderMock.Object);

            // Act
            await emailNotificationService.SendDailyNotificationsAsync();

            // Assert
            emailSenderMock.Verify(e => e.SendEmailAsync(
                "admin@company.com",
                "Upcoming Product Expiration",
                $"The product {product1.Name} is expiring on {product1.ExpiryDate:yyyy-MM-dd}."), Times.Once);

            emailSenderMock.Verify(e => e.SendEmailAsync(
                "admin@company.com",
                "Upcoming Product Expiration",
                $"The product {product2.Name} is expiring on {product2.ExpiryDate:yyyy-MM-dd}."), Times.Never);
        }

        [Fact]
        public async Task SendDailyNotificationsAsync_NoUpcomingExpirations()
        {
            // Arrange
            var productServiceMock = new Mock<IProductService>();
            var emailSenderMock = new Mock<IEmailSender>();

            var product1 = new Product { Name = "Product1", ExpiryDate = DateTime.UtcNow.AddDays(10) };
            var product2 = new Product { Name = "Product2", ExpiryDate = DateTime.UtcNow.AddDays(12) };

            var products = new List<Product> { product1, product2 };

            productServiceMock.Setup(p => p.GetAllProductsAsync()).ReturnsAsync(products);

            var emailNotificationService = new EmailNotificationService(productServiceMock.Object, emailSenderMock.Object);

            // Act
            await emailNotificationService.SendDailyNotificationsAsync();

            // Assert
            emailSenderMock.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }

}
