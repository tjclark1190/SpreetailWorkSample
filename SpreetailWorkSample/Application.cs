using SpreetailWorkSample.Attributes;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpreetailWorkSample
{
    public class Application
    {              
        private readonly IServiceProvider _serviceProvider;
        private readonly KeyValuePairService<string,string> _keyValuePairService;

        public Application(KeyValuePairService<string, string> keyValuePairService)
        {
            _serviceProvider = ServiceProviderFactory.ServiceProvider;
            _keyValuePairService = keyValuePairService;
        }

        public void Run()
        {
            var input = string.Empty;

            while (!input.Equals(ApplicationConstants.Commands.Exit, StringComparison.InvariantCultureIgnoreCase))
            {
                //Add try catch
                try
                {
                    input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                        continue;

                    //Input is delimited by a single space. It is used in this order command, key, value.
                    var inputArray = input.Split(ConfigurationHelper.InputDelimiter);

                    if (inputArray == null || !inputArray.Any())
                        throw new Exception("InputArray does not have any elements");

                    var command = inputArray[0];

                    if (string.IsNullOrWhiteSpace(command))
                        throw new Exception("Command is required");

                    var key = inputArray.Length > 1 ? inputArray[1] : string.Empty;
                    var value = inputArray.Length > 2 ? inputArray[2] : string.Empty;

                    _keyValuePairService.CommandSwitch(command, key, value);

                    if (_keyValuePairService.OutputBuilder.Length > 0)
                        Console.WriteLine(_keyValuePairService.OutputBuilder.ToString());

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error, {ex.Message}");
                }

                Console.WriteLine();
            }
        }


        //public List<string> ExecuteCommand(string command, string key, string value)
        //{
        //    var assembly = Assembly.GetAssembly(typeof(KeyValuePairService<string, string>));

        //    var methods = TypeExtensions.GetMethods(typeof(KeyValuePairService<string, string>))
        //                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0);

        //    foreach (var m in methods)
        //    {
        //        var constructorArguements = m.CustomAttributes.SelectMany(ca => ca.ConstructorArguments).ToList();

        //        //Check if command of "KEYS" exists as one of the command attributes on methods
        //        if (!constructorArguements.Any(arg => arg.Value.IsEqual(command)))
        //            continue;

        //        var result = m.Invoke(_keyValuePairService, new object[] { key, value });
        //        //execute method
        //        //_processor.GetList<List<string>>((Func<List<string>>)m);

        //        //foreach(var ca in m.CustomAttributes)
        //        //{
        //        //    var commandExists = ca.ConstructorArguments.Any(arg=> arg.Value.IsEqual(ApplicationConstants.Commands.Keys));
        //        //}
        //    }
        //}
    }
}
