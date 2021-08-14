using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;

namespace SpreetailWorkSample
{
    class Program
    {
        private static IServiceProvider ServiceProvider;

        static void Main(string[] args)
        {
            ////setup the static class ConfigurationManager
            //RuntimeHelpers.RunClassConstructor(typeof(ConfigurationManager).TypeHandle);
            Console.WriteLine("Initializing...");

            //this will invoke the static constructor in the static class ServiceProviderFactory
            //IServiceCollection will be instantiated
            ServiceProvider = ServiceProviderFactory.ServiceProvider;

            var application = ServiceProvider.GetService<Application>();

            // entry to run app
            application.Run();
        }
    }
}
