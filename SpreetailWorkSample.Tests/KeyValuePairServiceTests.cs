using NUnit.Framework;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
//using SpreetailWorkSample.Processors.Mocks;

namespace SpreetailWorkSample.Tests
{
    [TestFixture]
    public class KeyValuePairServiceTests
    {
        //private KeyValuePairProcessorMock _processorMock;
        private readonly IServiceProvider _serviceProvider;
        private readonly KeyValuePairDisplayService<string, string> _processor;
          
        [SetUp]
        public void Setup()
        {
            //_processorMock = new KeyValuePairProcessorMock();
        }

        public KeyValuePairServiceTests()
        {
            _serviceProvider = ServiceProviderFactory.ServiceProvider;

            _processor = _serviceProvider.GetService<KeyValuePairDisplayService<string, string>>();
        }
       

        [Test]
        public void AddCommandTestSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var output = _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, "apple", "tree");

            Assert.AreNotEqual(0, keyValuePairs.Count);                               
            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, output);
        }

        [Test]
        public void AddCommandTestNullKeyFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, null, "tree"));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);           
        }

        [Test]
        public void AddCommandTestEmptyKeyFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, string.Empty, "tree"));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);
        }

        [Test]
        public void AddCommandTestNullMemberFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, "apple", null));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }

        [Test]
        public void AddCommandTestEmptyMemberFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, "apple", string.Empty));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        } 
        
        [Test]
        public void GetKeysForDisplayResultReturnedSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Add, "apple", "tree");

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput);

            var keysOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, ApplicationConstants.Commands.Keys, null, null);

            Assert.AreNotEqual(0, keyValuePairs.Count);
            Assert.AreNotEqual(0, keysOutput.Length);            
        }      
    }
}
