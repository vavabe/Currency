using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyServices.UserApp.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GetJwtToken(Guid id, string username);
        Task<bool> InvalidateToken(string token);
    }
}
