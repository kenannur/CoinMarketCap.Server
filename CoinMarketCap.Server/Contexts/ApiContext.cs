using CoinMarketCap.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoinMarketCap.Server.Contexts
{
    public class ApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }        
    }
}
