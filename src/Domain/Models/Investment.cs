using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Product Product { get; set; }
    }


}
