using SpreetailWorkSample.Extensions;
using SpreetailWorkSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreetailWorkSample.Services
{
    public class KeyValuePairDisplayService<TKey, TMember>
    {
        public string KeyValuePairCommandOutput(List<KeyValuePair<TKey, TMember>> keyValuePairs, string command, TKey key, TMember member)
        {
            switch (command.ToUpper())
            {
                case ApplicationConstants.Commands.Keys:
                    return keyValuePairs.GetKeyValuePairListKeysForDisplay();
                case ApplicationConstants.Commands.Members:
                    return keyValuePairs.GetKeyValuePairListMembersForDisplay(key);
                case ApplicationConstants.Commands.Add:                   
                    return keyValuePairs.AddKeyValuePair(new KeyValuePair<TKey, TMember>(key, member));
                case ApplicationConstants.Commands.Remove:
                    //REMOVE foo bar
                    return keyValuePairs.RemoveKeyValuePairFromList(key, member);
                case ApplicationConstants.Commands.RemoveAll:
                    //REMOVEALL foo
                    return keyValuePairs.RemoveKeyValuePairsForKeyFromList(key);
                case ApplicationConstants.Commands.Clear:
                    return keyValuePairs.ClearKeyValuePairList();
                case ApplicationConstants.Commands.KeyExists:
                    return keyValuePairs.KeyExists(key);
                case ApplicationConstants.Commands.MemberExists:
                    return keyValuePairs.KeyValuePairExists(new KeyValuePair<TKey, TMember>(key, member));
                case ApplicationConstants.Commands.AllMembers:
                    return keyValuePairs.GetKeyValuePairListMembersForDisplay(key);
                case ApplicationConstants.Commands.Items:
                    return keyValuePairs.GetKeyValuePairListItemsForDisplay();
                case ApplicationConstants.Commands.Exit:          
                    return ApplicationConstants.SuccessMessages.ClosingApplication;
                default:
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.InvalidCommand, command));
            }
        }        
    }
}

namespace SpreetailWorkSample.Services.Mocks
{
    public class KeyValuePairServiceMock
    {
        private KeyValuePairDisplayService<string, string> _target;

        public KeyValuePairServiceMock()
        {
            _target = new KeyValuePairDisplayService<string, string>();
        }

        //public void GetInputKeyValuePairMembersForDisplay(string command, string key, string value)
        //{
        //    _target.AddKeyValuePairToKeyValuePairList(key, value);
        //}       
    }
}