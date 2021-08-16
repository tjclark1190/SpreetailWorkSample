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
        private readonly IServiceProvider _serviceProvider;
        private readonly KeyValuePairService<string, string> _processor;
         
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
        public void KeysWithEmptySetSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var keysOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(string.Empty, string.Empty), ApplicationConstants.Commands.Keys);

            Assert.AreEqual(0, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.EmptySet, keysOutput);
        }

        //MEMBERS
        //  1. Return: "Key is required"
        [Test]
        public void MembersWithRequiredKeyFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            var keyValuePairToProcess = new KeyValuePair<string, string>(string.Empty, string.Empty);

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, keyValuePairToProcess, ApplicationConstants.Commands.Members));

            Assert.AreEqual(0, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);
        }

        //  2. Return: "Key does not exist"
        [Test]
        public void MembersWithKeyDoesNotExistFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            var keyValuePairToProcess = new KeyValuePair<string, string>("foo", string.Empty);

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, keyValuePairToProcess, ApplicationConstants.Commands.Members));

            Assert.AreEqual(0, keyValuePairs.Count);
            Assert.AreEqual(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, keyValuePairToProcess.Key), ex.Message);
        }

        //  3. Result Set: "1) bar 2) baz"
        [Test]
        public void MembersWithResultSetSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var memberOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", string.Empty), ApplicationConstants.Commands.Members);

            memberOutput = memberOutput.Replace(Environment.NewLine, " ").Trim();

            Assert.AreEqual(2, keyValuePairs.Count);
            Assert.AreEqual("1) bar 2) baz", memberOutput);
        }

        //ALLMEMBERS
        //  1. Return: "(empty set)"
        [Test]
        public void AllMembersWithEmptySetSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();
            var keyValuePairToProcess = new KeyValuePair<string, string>(string.Empty, string.Empty);

            var memberOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, keyValuePairToProcess, ApplicationConstants.Commands.AllMembers);

            Assert.AreEqual(0, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.EmptySet, memberOutput);
        }

        //  2. Result Set: "1) bar 2) bar 3) bar"
        [Test]
        public void AllMembersWithResultSetSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var memberOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>(string.Empty, string.Empty), ApplicationConstants.Commands.AllMembers);

            memberOutput = memberOutput.Replace(Environment.NewLine, " ").Trim();

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual("1) bar 2) baz 3) bar", memberOutput);
        }

        //REMOVE
        //  1. RequiredKey
        [Test]
        public void RemoveWithKeyRequiredFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>(string.Empty, "bar");

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Remove));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);

        }

        //  2. Key does not exist
        [Test]
        public void RemoveWithKeyDoesNotExistFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("baz", "bar");

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Remove));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, toRemove.Key), ex.Message);
        }

        //  3. RequiredMember
        [Test]
        public void RemoveWithRequiredMemberFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("bang", string.Empty);

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Remove));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredMember, ex.Message);
        }

        //  4. Member does not exist
        [Test]
        public void RemoveWithMemberDoesNotExistFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("bang", "boom");

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Remove));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(string.Format(ApplicationConstants.ErrorMessages.MemberDoesNotExist, toRemove.Value), ex.Message);
        }

        //  5. Removed
        [Test]
        public void RemoveKeyValuePairSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("foo", "baz");

            var removeOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Remove);

            Assert.AreEqual(2, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.SuccessMessages.Removed, removeOutput);
        }

        //REMOVEALL
        //  1. RequiredKey
        [Test]
        public void RemoveAllWithKeyRequiredFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>(string.Empty, string.Empty);

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.RemoveAll));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.ErrorMessages.RequiredKey, ex.Message);

        }

        //  2. Key does not exist
        [Test]
        public void RemoveAllWithKeyDoesNotExistFailure()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("baz", string.Empty);

            var ex = Assert.Throws<Exception>(() => _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.RemoveAll));

            Assert.AreEqual(3, keyValuePairs.Count);
            Assert.AreEqual(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, toRemove.Key), ex.Message);
        }

        //  3. Removed
        [Test]
        public void RemoveAllKeyValuePairSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>("foo", string.Empty);

            var removeOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.RemoveAll);

            Assert.AreEqual(1, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.SuccessMessages.Removed, removeOutput);
        }

        //CLEAR
        //  1. "Cleared"
        [Test]
        public void ClearKeyValuePairsSuccess()
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>();

            var addOutput1 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput1);

            var addOutput2 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("foo", "baz"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput2);

            var addOutput3 = _processor.KeyValuePairCommandOutput(keyValuePairs, new KeyValuePair<string, string>("bang", "bar"), ApplicationConstants.Commands.Add);

            Assert.AreEqual(ApplicationConstants.SuccessMessages.Added, addOutput3);

            var toRemove = new KeyValuePair<string, string>(string.Empty, string.Empty);

            var removeOutput = _processor.KeyValuePairCommandOutput(keyValuePairs, toRemove, ApplicationConstants.Commands.Clear);

            Assert.AreEqual(0, keyValuePairs.Count);
            Assert.AreEqual(ApplicationConstants.SuccessMessages.Cleared, removeOutput);
        }

        //KEYEXISTS
        //  1. "true"
        //  2. "false"
        //MEMBEREXISTS
        //  1. "true"
        //  2. "false"
        //ITEMS
        //  1. "(empty set)"
        //  2. "1) foo: bar 2) bang: bar"       
    }
}
