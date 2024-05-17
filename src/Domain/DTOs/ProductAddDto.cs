using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpiryDate { get; set; }
    }

}
