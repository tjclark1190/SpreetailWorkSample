using NUnit.Framework;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Moq;
using SpreetailWorkSample.Services.Mocks;

namespace SpreetailWorkSample.Tests
{
    [TestFixture]
    public class KeyValuePairServiceMoqTests
    {
        //private KeyValuePairProcessorMock _processorMock;
        //private readonly IServiceProvider _serviceProvider;
        private readonly Mock<KeyValuePairServiceMock> _moqProcessor;
          
//        [SetUp]
//        public void Setup()
//        {
//            //_processorMock = new KeyValuePairProcessorMock();

////            _processor = new Mock<KeyValuePairProcessor<string, string>>();
//        }

        public KeyValuePairServiceMoqTests()
        {
            _moqProcessor = new Mock<KeyValuePairServiceMock>();
        }


        [Test]
        public void AddCommandTestSuccess()
        {
            //_moqProcessor.Setup(x => x.GetInputKeyValuePairMembersForDisplay());

            //_processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", "tree");

            

            //Assert.AreNotEqual(0, _moqProcessor.KeyValuePairList.Count);
            //Assert.AreNotEqual(0, _moqProcessor.OutputBuilder.Length);                       
            //Assert.AreEqual("Added", _moqProcessor.OutputBuilder.ToString());
        }

        [Test]
        public void AddCommandTestNullKeyFailure()
        {
            //var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, null, "tree"));

            //Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);           
        }

        [Test]
        public void AddCommandTestEmptyKeyFailure()
        {
            //var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, string.Empty, "tree"));

            //Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);
        }

        [Test]
        public void AddCommandTestNullMemberFailure()
        {
            //var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", null));

            //Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }

        [Test]
        public void AddCommandTestEmptyMemberFailure()
        {
            //var ex = Assert.Throws<Exception>(() => _processor.CommandSwitch(ApplicationConstants.Commands.Add, "apple", string.Empty));

            //Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }
    }
}
