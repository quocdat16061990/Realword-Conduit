using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatDotnetConduit.Domain.Entities;

namespace DatDotnetConduit.Infrasturcture.Auth
{
    internal class AuthService : IAuthService
    {
        private const int ACCESS_TOKEN_LIFE_TIME = 2;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var credential = _configuration["AppCredential"];
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(credential);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"{user.Username}"),
                    new Claim(ClaimTypes.NameIdentifier, $"{user.Id}")
                }),
                Expires = DateTime.UtcNow.AddHours(ACCESS_TOKEN_LIFE_TIME),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashPassword, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashPassword);
        }
    }
}
