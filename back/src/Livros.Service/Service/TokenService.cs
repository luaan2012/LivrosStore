using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Livros.Domain.Models;
using Livros.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Livros.Service.Service
{
    public class TokenService : ITokenService
    {
        private readonly User _user;
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration, User user)
        {
            _configuration = configuration;
            _user = user;  
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}