using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IInvestmentRepository
    {
        void AddTransaction(InvestmentTransaction transaction);
        List<InvestmentTransaction> GetTransactionsByProductId(int productId);
    }
}
