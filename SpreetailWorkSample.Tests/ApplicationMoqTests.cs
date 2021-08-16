using NUnit.Framework;
using SpreetailWorkSample.Configuration;
using SpreetailWorkSample.Helpers;
using SpreetailWorkSample.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Moq;
using SpreetailWorkSample.Services.Mocks;
using System.Collections.Generic;

namespace SpreetailWorkSample.Tests
{
    [TestFixture]
    public class ApplicationMoqTests
    {
        private readonly Mock<Application> _moqProcessor;
          
        public ApplicationMoqTests()
        {
            _moqProcessor = new Mock<Application>();
        }

        [Test]
        public void ProcessUserInputSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var input = "Add foo bar";

            _moqProcessor.Setup(x=>x.ProcessUserInput(keyValuePairs, input))
                .Returns("");
        }
    }
}
