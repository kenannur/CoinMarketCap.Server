using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoinMarketCap.Server.Contexts;
using CoinMarketCap.Server.Interfaces;
using CoinMarketCap.Server.Models.Custom;
using CoinMarketCap.Server.Models.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoinMarketCap.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApiContext _apiContext;
        private readonly IJwtSettings _jwtSettings;

        public AccountService(ApiContext apiContext, IJwtSettings jwtSettings)
        {
            _apiContext = apiContext;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> LoginAsync(LoginRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("Login Request is null");
            }

            var user = await _apiContext.Users
                .FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Password == request.Password);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return CreateToken();
        }

        private string CreateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "User")
                },
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutesFromNow),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return jwtToken;
        }
    }
}
