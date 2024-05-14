using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IProductService
    {
        List<InvestmentProduct> GetAllProducts();
        InvestmentProduct GetProductById(int id);
        void CreateProduct(InvestmentProduct product);
        void UpdateProduct(InvestmentProduct product);
        void DeleteProduct(int id);
    }
}
