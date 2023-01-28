using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cybersoft.Authentication
{
    public interface IJwtAuthenticationManager
    {
        JwtResponse Authenticate(bool isAuthenticated, UserModel user);

    }
}
