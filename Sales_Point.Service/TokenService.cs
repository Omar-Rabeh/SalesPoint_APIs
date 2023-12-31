﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Sales_Point.Core.Entities.Identity;
using Sales_Point.Core.Service;

namespace Sales_Point.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _Configuration;

        public TokenService(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            //PayLoad data Claim

            //private Claims

            var AuthClaim = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };

            //register Claims

            // Key
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:Key"]));

            var Token = new JwtSecurityToken(
                issuer: _Configuration["JWT:ValidIssuer"],
                audience: _Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(_Configuration["JWT:DurationInDays"])),
                claims: AuthClaim,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256)
                ) ;

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }


}
