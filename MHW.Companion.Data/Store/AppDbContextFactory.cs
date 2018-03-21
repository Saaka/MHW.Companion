using MHW.Companion.Config;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MHW.Companion.Data.Store
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(GetConnectionString(),
                opt =>
                {
                    opt.MigrationsHistoryTable("MHWCompationMigrations");
                });

            return new AppDbContext(optionsBuilder.Options);
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("dbsettings.json");

            var config = builder.Build();
            return config["data:connectionString"].ToString();
        }
    }
}
