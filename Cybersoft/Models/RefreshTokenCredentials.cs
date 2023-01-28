using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Models
{
    public class RefreshTokenCredentials
    {
        public string jwtToken { get; set; }
        public string refreshToken{ get; set; }
    }
}
