using System;
using System.Linq;
using CoinMarketCap.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoinMarketCap.Server.Contexts
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiContext(serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.Add(new User
                    {
                        Id = 1,
                        UserName = "kenan",
                        Password = "1234"
                    });

                context.SaveChanges();
            }
        }
    }
}
