using SpreetailWorkSample.Extensions;
using SpreetailWorkSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreetailWorkSample.Services
{
    public class KeyValuePairService<TKey, TMember>
    {
        public string KeyValuePairCommandOutput(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePairToProcess, string command)
        {
            return command switch
            {
                ApplicationConstants.Commands.Keys => Keys(keyValuePairs),
                ApplicationConstants.Commands.Members => Members(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.Add => Add(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.Remove => Remove(keyValuePairs, keyValuePairToProcess),//REMOVE foo bar
                ApplicationConstants.Commands.RemoveAll => RemoveAll(keyValuePairs, keyValuePairToProcess),//REMOVEALL foo
                ApplicationConstants.Commands.Clear => Clear(keyValuePairs),
                ApplicationConstants.Commands.KeyExists => KeyExists(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.MemberExists => MemberExists(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.AllMembers => Members(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.Items => Items(keyValuePairs),
                ApplicationConstants.Commands.Exit => ApplicationConstants.SuccessMessages.ClosingApplication,
                _ => throw new Exception(string.Format(ApplicationConstants.ErrorMessages.InvalidCommand, command)),
            };
        }

        //KEYS
        private string Keys(List<KeyValuePair<TKey, TMember>> keyValueList)
        {
            if (keyValueList == null || !keyValueList.Any())
            {
                return ApplicationConstants.ErrorMessages.EmptySet;
            }

            var keys = (from kvp in keyValueList select kvp.Key).Distinct().ToList();

            var displayBuilder = new StringBuilder();

            var listNumber = 0;

            foreach (var key in keys)
            {
                listNumber++;

                displayBuilder.AppendLine($"{listNumber}) {key}");
            }

            return displayBuilder.ToString();
        }

        //KEYEXISTS
        private string KeyExists(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            //keyValuePairs.FilterKeyValuePairListByKey(key);

            if (keyValuePairs.FilterKeyValuePairListByKey(keyValuePair.Key).Any())
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //MEMBERS
        private string Members(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            //if key is not null then check it exists in KeyValuePairList
            if (!keyValuePair.Key.CheckNullOrEmpty())
            {
                keyValuePairs = keyValuePairs.FilterKeyValuePairListByKey(keyValuePair.Key);

                if (!keyValuePairs.Any())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, keyValuePair.Key));
            }

            if (keyValuePairs == null || !keyValuePairs.Any())
            {
                return ApplicationConstants.ErrorMessages.EmptySet;
            }

            var members = from kvp in keyValuePairs select kvp.Value;

            var displayBuilder = new StringBuilder();

            var listNumber = 0;
            foreach (var member in members)
            {
                listNumber++;

                displayBuilder.AppendLine($"{listNumber}) {member}");
            }

            return displayBuilder.ToString();
        }

        //MEMBEREXISTS
        private string MemberExists(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            if (keyValuePairs.Contains(keyValuePair))
            {
                return ApplicationConstants.SuccessMessages.True;
            }
            else
            {
                return ApplicationConstants.SuccessMessages.False;
            }
        }

        //ADD
        private string Add(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
        {
            if (keyValuePairs.AddKeyValuePair(toAdd))
                return ApplicationConstants.SuccessMessages.Added;

            return ApplicationConstants.ErrorMessages.AddFailed;
        }

        //ITEMS
        private string Items(List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            if (keyValuePairs == null || !keyValuePairs.Any())
            {
                return ApplicationConstants.ErrorMessages.EmptySet;
            }

            var displayBuilder = new StringBuilder();

            var listNumber = 0;
            foreach (var kvp in keyValuePairs)
            {
                listNumber++;

                displayBuilder.AppendLine($"{listNumber}) {kvp.Key}: {kvp.Value}");
            }

            return displayBuilder.ToString();
        }

        //CLEAR
        private string Clear(List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            keyValuePairs.Clear();

            return ApplicationConstants.SuccessMessages.Cleared;
        }

        //REMOVE foo bar
        private string Remove(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toRemove)
        {
            if (toRemove.Key.CheckNullOrEmpty())
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);

            //filter by "key"
            var keyValuePairsForKey = keyValuePairs.FilterKeyValuePairListByKey(toRemove.Key);

            if (!keyValuePairsForKey.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, toRemove.Key));

            //filter by "member"
            var keyValuePairsForMember = keyValuePairsForKey.FilterKeyValuePairListByMember(toRemove.Value); //(from kvp in keyValuePairsForKey where kvp.Value.IsEqual(member) select kvp).ToList();

            if (!keyValuePairsForMember.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.MemberDoesNotExist, toRemove.Value));

            //pass in the list returned for key and member to remove 
            if (keyValuePairs.RemoveItemsFromKeyValuePairList(keyValuePairsForMember))
            {
                return ApplicationConstants.SuccessMessages.Removed;
            }
            else
            {
                return ApplicationConstants.ErrorMessages.RemoveFailed;
            }
        }

        //REMOVEALL foo
        private string RemoveAll(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair)
        {
            var keyValuePairsForKey = keyValuePairs.FilterKeyValuePairListByKey(keyValuePair.Key);

            if (!keyValuePairsForKey.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, keyValuePair.Key));

            //pass in the list returned for key to remove 
            if (keyValuePairs.RemoveItemsFromKeyValuePairList(keyValuePairsForKey))
            {
                return ApplicationConstants.SuccessMessages.Removed;
            }
            else
            {
                return ApplicationConstants.ErrorMessages.RemoveFailed;
            }
        }
    }
}

namespace SpreetailWorkSample.Services.Mocks
{
    public class KeyValuePairServiceMock
    {
        private readonly KeyValuePairService<string, string> _target;

        public KeyValuePairServiceMock()
        {
            _target = new KeyValuePairService<string, string>();
        }

        //public void Keys(List<KeyValuePair<string, string>> keyValuePairs)
        //{
        //    _target.Keys(keyValuePairs);
        //}
    }
}