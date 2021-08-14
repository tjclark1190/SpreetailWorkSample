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
        public string KeyValuePairCommandOutput(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePairToProcess, string command)
        {
            switch (command.ToUpper())
            {
                case ApplicationConstants.Commands.Keys:
                    return Keys(keyValuePairs);
                case ApplicationConstants.Commands.Members:
                    return Members(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.Add:
                    return Add(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.Remove:
                    //REMOVE foo bar
                    return Remove(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.RemoveAll:
                    //REMOVEALL foo
                    return RemoveAll(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.Clear:
                    return Clear(keyValuePairs);
                case ApplicationConstants.Commands.KeyExists:
                    return KeyExists(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.MemberExists:
                    return MemberExists(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.AllMembers:
                    return AllMembers(keyValuePairs, keyValuePairToProcess);
                case ApplicationConstants.Commands.Items:
                    return Items(keyValuePairs);
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

        private string Members(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            return keyValuePairs.GetKeyValuePairListMembersForDisplay(keyValuePair.Key);
        }

        private string Add(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
        {
            if(keyValuePairs.AddKeyValuePair(toAdd))
                return ApplicationConstants.SuccessMessages.Added;

            return ApplicationConstants.ErrorMessages.AddFailed;
        }

        private string Remove(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toRemove)
        {
            return keyValuePairs.RemoveKeyValuePairFromList(toRemove);
        }

        private string RemoveAll(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toRemove)
        {
            return keyValuePairs.RemoveKeyValuePairsForKeyFromList(toRemove.Key);
        }

        private string Clear(List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            return keyValuePairs.ClearKeyValuePairList();
        }

        private string KeyExists(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            return keyValuePairs.KeyExists(keyValuePair.Key);
        }
        
        private string MemberExists(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            return keyValuePairs.KeyValuePairExists(keyValuePair);
        }

        private string AllMembers(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            return keyValuePairs.GetKeyValuePairListMembersForDisplay(keyValuePair.Key);
        }

        private string Items(List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            return keyValuePairs.GetKeyValuePairListItemsForDisplay();
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