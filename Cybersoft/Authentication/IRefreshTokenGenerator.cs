using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cybersoft.Authentication
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
