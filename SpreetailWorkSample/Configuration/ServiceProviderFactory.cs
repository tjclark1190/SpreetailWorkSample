using Microsoft.Extensions.DependencyInjection;
using SpreetailWorkSample.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpreetailWorkSample.Configuration
{
    public static class ServiceProviderFactory
    {
        public static IServiceProvider ServiceProvider { get; }
        static ServiceProviderFactory()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            //add the class that executes the Application
            services.AddSingleton<Application>();

            //add processor
            services.AddSingleton<KeyValuePairService<string, string>>();            
        }
    }
}
