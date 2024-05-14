using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class InvestmentProductStatement
    {
        public InvestmentProduct Product { get; set; }
        public List<InvestmentTransaction> Transactions { get; set; }
    }

}
