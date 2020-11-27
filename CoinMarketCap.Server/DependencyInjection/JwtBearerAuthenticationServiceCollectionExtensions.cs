using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoinMarketCap.Server.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CoinMarketCap.Server.DependencyInjection
{
    public static class JwtBearerAuthenticationServiceCollectionExtensions
    {
        public static void AddJwtBearerAuthentication(this IServiceCollection services, IConfigurationSection jwtSection)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSection["Issuer"],
                            ValidAudience = jwtSection["Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]))
                        };

                        options.Events = new JwtBearerEvents
                        {
                            OnForbidden = context =>
                            {
                                return context.HttpContext.OverrideResponse(HttpStatusCode.Forbidden,
                                    "Lütfen giriş yapınız");
                            }
                        };
                    });
        }
    }
}
