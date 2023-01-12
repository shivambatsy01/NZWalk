using Microsoft.IdentityModel.Tokens;
using NZWalk.API.Models.Domain;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace NZWalk.API.Repositories.TokenRepository
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;
        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.Firstname),
                new Claim(ClaimTypes.Surname, user.Lastname),
                new Claim(ClaimTypes.Email, user.Email)
            };

            user.UserRoles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            });

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credential
                );

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
