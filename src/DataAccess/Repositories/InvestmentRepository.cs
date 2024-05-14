using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly List<InvestmentTransaction> _transactions = new();

        public void AddTransaction(InvestmentTransaction transaction) => _transactions.Add(transaction);

        public List<InvestmentTransaction> GetTransactionsByProductId(int productId)
            => _transactions.Where(t => t.ProductId == productId).ToList();
    }
}
