using DataAccess.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<InvestmentProduct> GetAllProducts() => _productRepository.GetAll();

        public InvestmentProduct GetProductById(int id) => _productRepository.GetById(id);

        public void CreateProduct(InvestmentProduct product) => _productRepository.Add(product);

        public void UpdateProduct(InvestmentProduct product) => _productRepository.Update(product);

        public void DeleteProduct(int id) => _productRepository.Delete(id);
    }

}
