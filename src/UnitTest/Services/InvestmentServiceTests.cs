using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace UnitTest.Services
{
    public class InvestmentServiceTests
    {
        [Fact]
        public async Task BuyProductAsync_ValidProductAndQuantity_Success()
        {
            // Arrange
            var mockProductRepository = new Mock<IProductRepository>();
            var mockInvestmentRepository = new Mock<IInvestmentRepository>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            var investmentService = new InvestmentService(mockInvestmentRepository.Object, mockProductRepository.Object, mockMemoryCache.Object);
            var productId = 1;
            var quantity = 5;
            var product = new Product { Id = productId, Name = "Test Product", Price = 100 };
            mockProductRepository.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            await investmentService.BuyProductAsync(productId, quantity);

            // Assert
            mockProductRepository.Verify(r => r.GetByIdAsync(productId), Times.Once);
            mockInvestmentRepository.Verify(r => r.BuyAsync(product, quantity), Times.Once);
            mockMemoryCache.Verify(c => c.Remove(It.IsAny<object>()), Times.Once);
        }

    }
}