using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Infrastructure.Options
{
    public class CacheOptions
    {
        public const string Name = "Cache";
        public string InvalidTokenSetName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;
    }
}
