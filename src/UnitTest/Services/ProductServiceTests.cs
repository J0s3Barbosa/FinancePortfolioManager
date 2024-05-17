using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Services;

namespace UnitTest.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetAllProductsAsync_CacheMiss_FetchesFromRepositoryAndCaches()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product" } };
            mockProductRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var mockMemoryCache = new Mock<IMemoryCache>();
            object cacheKey = null;
            mockMemoryCache.Setup(c => c.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny)).Returns(false);

            var cacheEntryMock = new Mock<ICacheEntry>();
            cacheEntryMock.SetupProperty(c => c.Value);
            mockMemoryCache.Setup(c => c.CreateEntry(It.IsAny<object>()))
                .Callback((object key) => cacheKey = key)
                .Returns(cacheEntryMock.Object);

            var productService = new ProductService(mockProductRepository.Object, mockMemoryCache.Object);

            var result = await productService.GetAllProductsAsync();

            Assert.Equal(products, result);
            mockProductRepository.Verify(r => r.GetAllAsync(), Times.Once);

            cacheEntryMock.VerifySet(c => c.Value = products, Times.Once);
            Assert.Equal("products_cache_key", cacheKey); // Check the cache key
        }

        [Fact]
        public async Task GetAllProductsAsync_CacheHit_ReturnsCachedData()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var products = new List<Product> { new Product { Id = 1, Name = "Test Product" } };

            var mockMemoryCache = new MockMemoryCache();
            var cacheEntry = mockMemoryCache.CreateEntry("products_cache_key");
            cacheEntry.Value = products;

            var productService = new ProductService(mockProductRepository.Object, mockMemoryCache);

            var result = await productService.GetAllProductsAsync();

            Assert.Equal(products, result);
            mockProductRepository.Verify(r => r.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task GetProductByIdAsync_ValidId_ReturnsProduct()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var product = new Product { Id = 1, Name = "Test Product" };
            mockProductRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            var productService = new ProductService(mockProductRepository.Object, Mock.Of<IMemoryCache>());

            var result = await productService.GetProductByIdAsync(1);

            Assert.Equal(product, result);
            mockProductRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetProductByIdAsync_InvalidId_ThrowsArgumentException()
        {
            var productService = new ProductService(Mock.Of<IProductRepository>(), Mock.Of<IMemoryCache>());

            await Assert.ThrowsAsync<ArgumentException>(() => productService.GetProductByIdAsync(0));
        }

        [Fact]
        public async Task CreateProductAsync_ValidProduct_CallsRepository()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var product = new Product { Id = 1, Name = "Test Product" };
            var productService = new ProductService(mockProductRepository.Object, Mock.Of<IMemoryCache>());

            await productService.CreateProductAsync(product);

            mockProductRepository.Verify(r => r.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task CreateProductAsync_NullProduct_ThrowsArgumentNullException()
        {
            var productService = new ProductService(Mock.Of<IProductRepository>(), Mock.Of<IMemoryCache>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.CreateProductAsync(null));
        }

        [Fact]
        public async Task UpdateProductAsync_ValidProduct_CallsRepository()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var product = new Product { Id = 1, Name = "Updated Product" };
            var productService = new ProductService(mockProductRepository.Object, Mock.Of<IMemoryCache>());

            await productService.UpdateProductAsync(product);

            mockProductRepository.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_NullProduct_ThrowsArgumentNullException()
        {
            var productService = new ProductService(Mock.Of<IProductRepository>(), Mock.Of<IMemoryCache>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.UpdateProductAsync(null));
        }

        [Fact]
        public async Task DeleteProductAsync_ValidId_CallsRepository()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            var productService = new ProductService(mockProductRepository.Object, Mock.Of<IMemoryCache>());

            await productService.DeleteProductAsync(1);

            mockProductRepository.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_InvalidId_ThrowsArgumentException()
        {
            var productService = new ProductService(Mock.Of<IProductRepository>(), Mock.Of<IMemoryCache>());

            await Assert.ThrowsAsync<ArgumentException>(() => productService.DeleteProductAsync(0));
        }
    }

}
