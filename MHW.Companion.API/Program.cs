using System;
using MHW.Companion.Data.Initializer;
using MHW.Companion.Data.Store;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MHW.Companion.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            InitDb(host);

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostCtx, config) =>
                {
                    config.AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"dbsettings.{hostCtx.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .Build();

        private static void InitDb(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<AppDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}
