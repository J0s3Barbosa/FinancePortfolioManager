using DataAccess.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly MainContext _context;

        public InvestmentRepository(MainContext context)
        {
            _context = context;
        }
   
        public async Task BuyAsync(Product product, int quantity)
        {
            var investment = new Investment
            {
                ProductId = product.Id,
                Amount = quantity,
                PurchaseDate = product.ExpiryDate,
            };
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();

        }

        public async Task SellAsync(Product product, int quantity)
        {
            // Find the investment corresponding to the product
            var investment = await _context.Investments
                                           .FirstOrDefaultAsync(i => i.ProductId == product.Id);

            // If the investment exists
            if (investment != null)
            {
                // Ensure the quantity to be sold is valid
                if (quantity <= 0)
                {
                    throw new ArgumentException("A quantidade deve ser maior que zero.");
                }

                // Reduce the quantity of the investment
                investment.Amount -= quantity;

                // If the quantity becomes zero or negative, remove the investment
                if (investment.Amount <= 0)
                {
                    _context.Investments.Remove(investment);
                }

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<Investment>> GetAllAsync()
        {
            return await _context.Investments.ToListAsync();
        }

        public async Task<Investment> GetByIdAsync(int id)
        {
            return await _context.Investments.FindAsync(id);
        }

        public async Task AddAsync(Investment investment)
        {
            _context.Investments.Add(investment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Investment investment)
        {
            _context.Investments.Update(investment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var investment = await _context.Investments.FindAsync(id);
            if (investment != null)
            {
                _context.Investments.Remove(investment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
