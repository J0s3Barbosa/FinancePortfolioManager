using Domain.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IProductService _productService;
        private readonly IInvestmentRepository _investmentRepository;

        public InvestmentService(IProductService productService, IInvestmentRepository investmentRepository)
        {
            _productService = productService;
            _investmentRepository = investmentRepository;
        }

        public void BuyProduct(int productId, int quantity)
        {
            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                var transaction = new InvestmentTransaction
                {
                    ProductId = productId,
                    Type = "Compra",
                    Quantity = quantity,
                    Amount = product.Price * quantity,
                    Date = DateTime.UtcNow
                };
                _investmentRepository.AddTransaction(transaction);
            }
        }

        public void SellProduct(int productId, int quantity)
        {
            var product = _productService.GetProductById(productId);
            if (product != null)
            {
                var transaction = new InvestmentTransaction
                {
                    ProductId = productId,
                    Type = "Venda",
                    Quantity = quantity,
                    Amount = product.Price * quantity,
                    Date = DateTime.UtcNow
                };
                _investmentRepository.AddTransaction(transaction);
            }
        }

        public InvestmentProductStatement GetProductStatement(int productId)
        {
            var product = _productService.GetProductById(productId);
            var transactions = _investmentRepository.GetTransactionsByProductId(productId);
            return new InvestmentProductStatement
            {
                Product = product,
                Transactions = transactions
            };
        }
    }

}
