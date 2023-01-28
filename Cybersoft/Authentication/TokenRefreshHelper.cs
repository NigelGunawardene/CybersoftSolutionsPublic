using Cybersoft.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Authentication
{
    public class TokenRefreshHelper
    {
        private IConfiguration _configuration { get; }

        public TokenRefreshHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CheckIfTokenIsValid(RefreshTokenCredentials credentials)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(credentials.jwtToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:SecretKey"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = _configuration.GetValue<string>("Jwt:ValidIssuer"),
                        ValidAudience = _configuration.GetValue<string>("Jwt:ValidAudience"),
                        ValidateLifetime = false, // adding this to not throw an error and instead only check if token is valid
                    }, out validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token passed");
                }
                var userName = principal.Identity.Name;
                if (userName != null)
                {
                    return userName;
                }
                else throw new SecurityTokenException("Invalid token passed");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SecurityTokenException("Invalid token passed");
            }
        }
    }
}
