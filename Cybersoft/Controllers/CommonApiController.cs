using Cybersoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cybersoft.Controllers
{
    public class CommonApiController : ControllerBase
    {
        //protected string UserName => User.Identity.Name.Split('@')[0];
        protected string UserName => User.Identity.Name;
        protected List<ClaimsIdentity> UserIdentities => User.Identities.ToList();
        protected List<Claim> UserClaims => UserIdentities[0].Claims.ToList();
        protected string UserRole => UserClaims[5].Value;
        protected bool isAuthenticated => User.Identity.IsAuthenticated;
    }
}
