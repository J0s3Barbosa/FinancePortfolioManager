using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Services.Interfaces;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private static readonly string cacheKey = "products_cache_key";
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private readonly object _cacheLock = new object();

        public ProductService(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            bool isAvaiable = _cache.TryGetValue(cacheKey, out IEnumerable<Product> products);
            if (isAvaiable) return products;
            else
            {
                try
                {
                    await semaphore.WaitAsync();
                    if (isAvaiable) return products;
                    else
                    {
                        products = await _productRepository.GetAllAsync();

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                .SetPriority(CacheItemPriority.Normal)
                                .SetSize(1024);

                        _cache.Set(cacheKey, products, cacheEntryOptions);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("Invalid product id.", nameof(id));
                }

                return await _productRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(product);

                Product result = await _productRepository.AddAsync(product);
                ClearCache();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(product);

                Product result = await _productRepository.UpdateAsync(product);
                ClearCache();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid product id.", nameof(id));
            }
            try
            {
                var entity = await _productRepository.GetByIdAsync(id);
                if (entity == null)
                    return false;

                await _productRepository.DeleteAsync(id);
                ClearCache(); // Clear cache after deleting data
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ClearCache()
        {
            lock (_cacheLock)
            {
                _cache.Remove(cacheKey);
            }
        }

    }

}
