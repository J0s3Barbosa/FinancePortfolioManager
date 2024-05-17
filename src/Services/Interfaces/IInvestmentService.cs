using Domain.Models;

namespace Services.Interfaces
{
    public interface IInvestmentService
    {
        Task BuyProductAsync(int productId, int quantity);
        Task SellProductAsync(int productId, int quantity);
        Task<IEnumerable<Investment>> GetAllInvestmentsAsync();
        Task<Investment> GetInvestmentByIdAsync(int id);
        Task CreateInvestmentAsync(Investment investment);
        Task UpdateInvestmentAsync(Investment investment);
        Task DeleteInvestmentAsync(int id);
    }

}
