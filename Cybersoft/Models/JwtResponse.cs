﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cybersoft.Models
{
    public class JwtResponse
    {
        public string jwtToken { get; set; }
        public string refreshToken { get; set; }
    }
}
