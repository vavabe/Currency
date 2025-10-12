using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Domain.Entities
{
    public class Currency
    {
        public string Name { get; set; } = string.Empty;
        public decimal Rate { get; set; }
    }
}
