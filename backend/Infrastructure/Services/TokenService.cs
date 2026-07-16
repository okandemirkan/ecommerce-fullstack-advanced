using Application.Interfaces;
using Application.Security;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key is not configured.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            //appsettingsjson'daki key'i çekiyoruz. token'in bize ait olup olmadığı kontrolü yapılıyor.
            //(security key)

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            //token imzalanırken hangi key ve o key'in imzalanma algoritması.

            var claims = new List<Claim> //token içeriği oluşturuluyor.
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()), //claims her zaman string alır.
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role.RoleName),
            };
            if (user.WorkspaceId.HasValue)
                claims.Add(new Claim(WorkspaceClaimTypes.WorkspaceId, user.WorkspaceId.Value.ToString()));

            var token = new JwtSecurityToken( //token oluşturuluyor. 
                issuer: _configuration["Jwt:Issuer"], //tokeni kim oluşturdu
                audience: _configuration["Jwt:Audience"], //token kimin için oluşturuldu
                claims: claims, //içerik
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                //jwt ömrü.
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token); //tokeni stringe çevirip return ediyor. 
        }
    }
}
