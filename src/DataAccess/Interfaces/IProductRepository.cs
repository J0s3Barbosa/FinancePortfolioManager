using Domain.Models;

namespace DataAccess.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);
    }
}
