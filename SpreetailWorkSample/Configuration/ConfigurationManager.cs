using System;
using System.IO;
using SpreetailWorkSample.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SpreetailWorkSample.Configuration
{
    public static class ConfigurationManager
    {
        public static IConfiguration Configuration { get; private set; }

        public static string EnvironmentName { get; private set; }

        public static readonly string EnvironmentKey = "ASPNETCORE_ENVIRONMENT";

        public static readonly string ApplicationBasePath = AppDomain.CurrentDomain.BaseDirectory;

        static ConfigurationManager()
        {
            //get the application root
            ApplicationBasePath = AppDomain.CurrentDomain.BaseDirectory;

            //setup the configuration for current environment  
            InitializeConfiguration();
        }

        private static void InitializeConfiguration()
        {          
            var configureBuilder = new ConfigurationBuilder()
                .SetBasePath(ApplicationBasePath)
                .AddJsonFile("appsettings.json")               
                .AddEnvironmentVariables();

            Configuration = configureBuilder.Build();

        }

        public static T GetValue<T>(string keyName, string configSection)
        {
            // Retrieve the value from the configuration file and perform the type conversion.
            var configStringValue = Configuration[$"{configSection}:{keyName}"];

            return (T)Convert.ChangeType(configStringValue, typeof(T));
        }
    }
}
