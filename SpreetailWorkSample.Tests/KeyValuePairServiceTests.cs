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
        private readonly KeyValuePairService<string, string> _processor;
          
        [SetUp]
        public void Setup()
        {
            //_processorMock = new KeyValuePairProcessorMock();
        }

        public KeyValuePairServiceTests()
        {
            _serviceProvider = ServiceProviderFactory.ServiceProvider;

            _processor = _serviceProvider.GetService<KeyValuePairService<string, string>>();
        }
       

        [Test]
        public void AddCommandTestSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var output = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("apple", "tree"), ApplicationConstants.Commands.Add);

            Assert.AreNotEqual(0, keyValuePairs.Count);                               
            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, output);
        }

        [Test]
        public void AddCommandTestNullKeyFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(null, "tree"), ApplicationConstants.Commands.Add));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);           
        }

        [Test]
        public void AddCommandTestEmptyKeyFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(string.Empty, "tree"), ApplicationConstants.Commands.Add));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);
        }

        [Test]
        public void AddCommandTestNullMemberFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("apple", null), ApplicationConstants.Commands.Add));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }

        [Test]
        public void AddCommandTestEmptyMemberFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("apple", string.Empty), ApplicationConstants.Commands.Add));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        } 
        
        [Test]
        public void KeysWithAddedListSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var keysOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(string.Empty, string.Empty), ApplicationConstants.Commands.Keys);

            keysOutput = keysOutput.Replace(Environment.NewLine, " ").Trim();

            Assert.AreNotEqual(0, keyValuePairs.Count);
            Assert.AreEqual("1) foo 2) bang", keysOutput);            
        }

        [Test]
        public void KeysWithEmptyListSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var keysOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(string.Empty, string.Empty), ApplicationConstants.Commands.Keys);

            Assert.AreNotEqual(0, keyValuePairs.Count);
            Assert.AreEqual(0, keysOutput);
        }
    }
}
