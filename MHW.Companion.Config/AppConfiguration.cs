using Microsoft.Extensions.Configuration;
using System;

namespace MHW.Companion.Config
{
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _config;

        public AppConfiguration(IConfiguration config)
        {
            _config = config;
        }
        public string DbConnectionString => _config["data:connectionString"].ToString();

        public string GetDbConnectionString()
        {
            return DbConnectionString;
        }
    }
}
