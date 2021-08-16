using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SpreetailWorkSample.Services.Interfaces;

namespace SpreetailWorkSample
{
    public class Application
    {
        private readonly IKeyValuePairService<string, string> _keyValuePairService;

        public Application(IKeyValuePairService<string, string> keyValuePairService)
        {            
            _keyValuePairService = keyValuePairService;
        }

        public void Run()
        {
            Console.Clear();

            var input = string.Empty;

            //declare memory based "dictionary" 
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            while (!input.Equals(ApplicationConstants.Commands.Exit, StringComparison.InvariantCultureIgnoreCase))
            {
                //Add try catch
                try
                {
                    input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                        continue;

                    var output = ProcessUserInput(keyValuePairs, input);

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

        public string ProcessUserInput(List<KeyValuePair<string, string>> keyValuePairs, string input)
        {
            //Input is delimited by a single space. It is used in this order command, key, value.
            var inputArray = input.Split(ConfigurationHelper.InputDelimiter);

            if (inputArray == null || !inputArray.Any())
                throw new Exception(ApplicationConstants.ErrorMessages.BlankInputArray);

            var command = inputArray[0]?.ToUpper();

            if (string.IsNullOrWhiteSpace(command))
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredCommand);

            var key = inputArray.Length > 1 ? inputArray[1] : string.Empty;
            var member = inputArray.Length > 2 ? inputArray[2] : string.Empty;

            return _keyValuePairService.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(key, member), command);
        }
    }
}

namespace SpreetailWorkSample.Services.Mocks
{    
    public class ApplicationMock
    {
        private readonly Application _target;

        public ApplicationMock()
        {
            var serviceProvider = ServiceProviderFactory.ServiceProvider;

            var keyValuePairService = serviceProvider.GetService<IKeyValuePairService<string, string>>();

            _target = new Application(keyValuePairService);
        }

        public string ProcessUserInput(List<KeyValuePair<string, string>> keyValuePairs, string input)
        {
            return _target.ProcessUserInput(keyValuePairs, input);
        }
    }
}
