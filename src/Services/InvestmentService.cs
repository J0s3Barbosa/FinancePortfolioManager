using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Repositories
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IInvestmentRepository _investmentRepository;
        private readonly IMemoryCache _cache;
        private static readonly string InvestmentCacheKey = "investments_cache_key";
        private readonly IProductRepository _productRepository;

        public InvestmentService(IInvestmentRepository investmentRepository, IProductRepository productRepository, IMemoryCache cache)
        {
            _investmentRepository = investmentRepository;
            _productRepository = productRepository;
            _cache = cache;
        }

        public async Task BuyProductAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || quantity <= 0)
            {
                throw new ArgumentException("Produto não encontrado ou quantidade inválida.");
            }

            // Perform the logic for buying the product asynchronously
            await _investmentRepository.BuyAsync(product, quantity);

            _cache.Remove(InvestmentCacheKey);
        }

        public async Task SellProductAsync(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null || quantity <= 0)
            {
                throw new ArgumentException("Produto não encontrado ou quantidade inválida.");
            }

            // Perform the logic for selling the product asynchronously
            await _investmentRepository.SellAsync(product, quantity);

            _cache.Remove(InvestmentCacheKey);
        }

        public async Task<IEnumerable<Investment>> GetAllInvestmentsAsync()
        {
            if (!_cache.TryGetValue(InvestmentCacheKey, out IEnumerable<Investment> investments))
            {
                investments = await _investmentRepository.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                _cache.Set(InvestmentCacheKey, investments, cacheEntryOptions);
            }

            return investments;
        }

        public async Task<Investment> GetInvestmentByIdAsync(int id)
        {
            var investment = await _investmentRepository.GetByIdAsync(id);
            if (investment == null)
            {
                throw new ArgumentException("Investimento não encontrado.");
            }
            return investment;
        }

        public async Task CreateInvestmentAsync(Investment investment)
        {
            if (investment == null)
            {
                throw new ArgumentNullException(nameof(investment), "Investimento não pode ser nulo.");
            }

            await _investmentRepository.AddAsync(investment);
            _cache.Remove(InvestmentCacheKey);
        }

        public async Task UpdateInvestmentAsync(Investment investment)
        {
            if (investment == null)
            {
                throw new ArgumentNullException(nameof(investment), "Investimento não pode ser nulo.");
            }

            await _investmentRepository.UpdateAsync(investment);
            _cache.Remove(InvestmentCacheKey);
        }

        public async Task DeleteInvestmentAsync(int id)
        {
            await _investmentRepository.DeleteAsync(id);
            _cache.Remove(InvestmentCacheKey);
        }
    }

}
