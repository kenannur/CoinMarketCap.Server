using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoinMarketCap.Server.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace CoinMarketCap.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ConfigureSerilogLogger();

                Log.Information("Starting WebHost");

                var host = Host.CreateDefaultBuilder(args)
                               .ConfigureLogging(logging => logging.ClearProviders())
                               .UseSerilog()
                               .ConfigureWebHostDefaults(webBuilder =>
                               {
                                   webBuilder.UseStartup<Startup>();
                               })
                               .Build();

                using (var scope = host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
                    DataGenerator.Initialize(scope.ServiceProvider);
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "WebHost terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureSerilogLogger()
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var directoryName = Path.GetDirectoryName(assemblyLocation);
            var logFilePath = $"{directoryName}/Log/errorLog.txt";

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.File(logFilePath)
                .CreateLogger();
        }
    }
}
