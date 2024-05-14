using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<InvestmentProduct> _products = new();

        public List<InvestmentProduct> GetAll() => _products;

        public InvestmentProduct GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(InvestmentProduct product) => _products.Add(product);

        public void Update(InvestmentProduct product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.ExpirationDate = product.ExpirationDate;
                existingProduct.Price = product.Price;
            }
        }

        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);
    }

}
