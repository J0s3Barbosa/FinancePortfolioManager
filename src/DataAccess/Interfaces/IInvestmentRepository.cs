using Domain.Models;

namespace DataAccess.Interfaces
{
    public interface IInvestmentRepository
    {
        Task BuyAsync(Product product, int quantity);
        Task SellAsync(Product product, int quantity);
        Task<IEnumerable<Investment>> GetAllAsync();
        Task<Investment> GetByIdAsync(int id);
        Task AddAsync(Investment investment);
        Task UpdateAsync(Investment investment);
        Task DeleteAsync(int id);
    }
}
