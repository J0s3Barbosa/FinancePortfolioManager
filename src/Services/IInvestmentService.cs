using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IInvestmentService
    {
        void BuyProduct(int productId, int quantity);
        void SellProduct(int productId, int quantity);
        InvestmentProductStatement GetProductStatement(int productId);
    }

}
