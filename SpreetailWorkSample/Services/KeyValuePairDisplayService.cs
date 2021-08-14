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
                    return Keys(keyValuePairs);//keyValuePairs.GetKeyValuePairListKeysForDisplay();
                case ApplicationConstants.Commands.Members:
                    return Members(keyValuePairs, key); //keyValuePairs.GetKeyValuePairListMembersForDisplay(key);
                case ApplicationConstants.Commands.Add:
                    return Add(keyValuePairs, new KeyValuePair<TKey, TMember> (key, member));
                case ApplicationConstants.Commands.Remove:
                    //REMOVE foo bar
                    return //keyValuePairs.RemoveKeyValuePairFromList(key, member);
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

        private string Keys(List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            return keyValuePairs.GetKeyValuePairListKeysForDisplay();
        }

        private string Members(List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key)
        {
            return keyValuePairs.GetKeyValuePairListMembersForDisplay(key);
        }

        private string Add(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
        {
            if(keyValuePairs.AddKeyValuePair(toAdd))
                return ApplicationConstants.SuccessMessages.Added;

            return ApplicationConstants.ErrorMessages.AddFailed;
        }

        private string Remove(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toRemove)
        {
            //if (keyValuePairs.AddKeyValuePair(toAdd))
            //    return ApplicationConstants.SuccessMessages.Added;

            return keyValuePairs.RemoveKeyValuePairFromList(toRemove);
        }

        private string RemoveAll(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
        {
            if (keyValuePairs.AddKeyValuePair(toAdd))
                return ApplicationConstants.SuccessMessages.Added;

            return ApplicationConstants.ErrorMessages.AddFailed;
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