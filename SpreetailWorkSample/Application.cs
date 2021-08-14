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
        private readonly KeyValuePairDisplayService<string, string> _keyValuePairService;

        public Application(KeyValuePairDisplayService<string, string> keyValuePairService)
        {
            _serviceProvider = ServiceProviderFactory.ServiceProvider;
            _keyValuePairService = keyValuePairService;
        }

        public void Run()
        {
            Console.Clear();

            var input = string.Empty;
            var keyValuePairs = new List<KeyValuePair<string, string>>();

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
                        throw new Exception(ApplicationConstants.ErrorMessages.BlankInputArray);

                    var command = inputArray[0];

                    if (string.IsNullOrWhiteSpace(command))
                        throw new Exception(ApplicationConstants.ErrorMessages.RequiredCommand);

                    var key = inputArray.Length > 1 ? inputArray[1] : string.Empty;
                    var member = inputArray.Length > 2 ? inputArray[2] : string.Empty;

                    var output = _keyValuePairService.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(key, member), command);

                    if (string.IsNullOrWhiteSpace(output))
                        throw new Exception(ApplicationConstants.ErrorMessages.BlankOutputString);

                    Console.WriteLine(output);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error, {ex.Message}");
                }

                Console.WriteLine();
            }
        } 
    }
}
