using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InvestmentTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; } // "Compra" ou "Venda"
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

}
