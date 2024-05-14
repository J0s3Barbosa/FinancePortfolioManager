using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IProductRepository
    {
        List<InvestmentProduct> GetAll();
        InvestmentProduct GetById(int id);
        void Add(InvestmentProduct product);
        void Update(InvestmentProduct product);
        void Delete(int id);
    }
}
