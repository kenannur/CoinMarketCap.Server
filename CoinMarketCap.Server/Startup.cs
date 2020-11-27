using System.Threading.Tasks;
using CoinMarketCap.Server.DependencyInjection;
using CoinMarketCap.Server.Interfaces;
using CoinMarketCap.Server.Middlewares;
using CoinMarketCap.Server.Models.Custom;
using CoinMarketCap.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CoinMarketCap.Server.Contexts;
using Microsoft.EntityFrameworkCore;
using CoinMarketCap.Server.Entities;

namespace CoinMarketCap.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddHttpClient<ICoinMarketCapService, CoinMarketCapService>();

            services.Configure<JwtSettings>(Configuration.GetSection(nameof(JwtSettings)));
            services.AddSingleton<IJwtSettings>(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

            services.AddDbContext<ApiContext>(options =>
            {
                options.UseInMemoryDatabase("ApiDb");
            });

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICryptoService, CryptoService>();

            services.AddJwtBearerAuthentication(Configuration.GetSection(nameof(JwtSettings)));

            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.WriteIndented = true;
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseGlobalExceptionHandler();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
