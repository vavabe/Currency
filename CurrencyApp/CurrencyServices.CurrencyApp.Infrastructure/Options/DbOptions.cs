using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.CurrencyApp.Infrastructure.Options
{
    public class DbOptions
    {
        public const string Name = "Db";
        public string ConnectionString { get; set; } = string.Empty;
    }
}
