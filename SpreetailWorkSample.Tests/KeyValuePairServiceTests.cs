using NUnit.Framework;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Linq;
using SpreetailWorkSample.Attributes;
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
            _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", "tree");

            Assert.AreNotEqual(0, _processor.KeyValuePairList.Count);
            Assert.AreNotEqual(0, _processor.OutputBuilder.Length);                       
            Assert.AreEqual("Added", _processor.OutputBuilder.ToString());
        }

        [Test]
        public void AddCommandTestNullKeyFailure()
        {
            var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, null, "tree"));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);           
        }

        [Test]
        public void AddCommandTestEmptyKeyFailure()
        {
            var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, string.Empty, "tree"));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);
        }

        [Test]
        public void AddCommandTestNullMemberFailure()
        {
            var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", null));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }

        [Test]
        public void AddCommandTestEmptyMemberFailure()
        {
            var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", string.Empty));

            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        } 
        
        [Test]
        public void GetKeysForDisplayResultReturnedSuccess()
        {
            _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", "tree");

            _processor.CommandSwitch(ApplicationConstants.Commands.Keys, null, null);

            Assert.AreNotEqual(0, _processor.KeyValuePairList.Count);
            Assert.AreNotEqual(0, _processor.OutputBuilder.Length);            
        }

        //[Test]
        //public void GetKeysForDisplayTestSuccess()
        //{
        //    //var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Keys, "apple", string.Empty));
        //    //_processor.GetKeyValuePairListKeysForDisplay();
        //    var assembly = Assembly.GetAssembly(typeof(KeyValuePairService<string, string>));

        //    var methods = TypeExtensions.GetMethods(typeof(KeyValuePairService<string, string>))
        //                .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0);

        //    foreach(var m in methods)
        //    {
        //        var constructorArguements = m.CustomAttributes.SelectMany(ca => ca.ConstructorArguments).ToList();

        //        //Check if command of "KEYS" exists as one of the command attributes on methods
        //        if (!constructorArguements.Any(arg => arg.Value.IsEqual(ApplicationConstants.Commands.Keys)))
        //            continue;

        //        var result = m.Invoke(_processor, new object[] { "apple", string.Empty });
        //        //execute method
        //        //_processor.GetList<List<string>>((Func<List<string>>)m);

        //        //foreach(var ca in m.CustomAttributes)
        //        //{
        //        //    var commandExists = ca.ConstructorArguments.Any(arg=> arg.Value.IsEqual(ApplicationConstants.Commands.Keys));
        //        //}
        //    }
        //    //var methods = assembly.GetMethods()                      
        //    //          .Where(m => m.GetCustomAttributes(typeof(MenuItemAttribute), false).Length > 0)
        //    //          .ToArray();

        //    //Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        //}
    }
}
