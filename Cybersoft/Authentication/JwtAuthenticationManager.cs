using Cybersoft.ApplicationCore.Entities;
using Cybersoft.ApplicationCore.Enums;
using Cybersoft.ApplicationCore.Models;
using Cybersoft.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private const int Days = 7;
        private const int Months = 3;
        private readonly string key;
        private readonly IRefreshTokenGenerator refreshTokenGenerator;

        public JwtAuthenticationManager(string key, IRefreshTokenGenerator refreshTokenGenerator)
        {
            this.key = key;
            this.refreshTokenGenerator = refreshTokenGenerator;
        }
        public JwtResponse Authenticate(bool isAuthenticated, UserModel user)
        {
            if (isAuthenticated)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim(ClaimTypes.Email, user.Email),
                    //new Claim(ClaimTypes.GivenName, user.FirstName),
                    //new Claim(ClaimTypes.Surname, user.LastName),
                    //new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(Days), //AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = refreshTokenGenerator.GenerateToken();
                JwtResponse jwtResponse = new JwtResponse
                {
                    jwtToken = tokenHandler.WriteToken(token),
                    refreshToken = refreshToken
                };
                return jwtResponse;
            }
            else return null;
        }
    }
}
