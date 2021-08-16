using SpreetailWorkSample.Extensions;
using SpreetailWorkSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreetailWorkSample.Services.Interfaces;

namespace SpreetailWorkSample.Services
{
    public class KeyValuePairService<TKey, TMember>: IKeyValuePairService<TKey, TMember>
    {
        public string KeyValuePairCommandOutput(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePairToProcess, string command)
        {
            return command switch
            {
                ApplicationConstants.Commands.Keys => Keys(keyValuePairs),
                ApplicationConstants.Commands.Members => Members(keyValuePairs, keyValuePairToProcess, filterByKey: true),
                ApplicationConstants.Commands.Add => Add(keyValuePairs, keyValuePairToProcess),
                ApplicationConstants.Commands.Remove => Remove(keyValuePairs, keyValuePairToProcess, filterByMember: true),//REMOVE foo bar
                ApplicationConstants.Commands.RemoveAll => Remove(keyValuePairs, keyValuePairToProcess),//REMOVEALL foo
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
                return ApplicationConstants.ErrorMessages.EmptySet;

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
        private string Members(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> keyValuePair, bool filterByKey = false)
        {
            //Members: filterByKey = true
            //AllMembers: filterByKey = false

            //if filterByKey is true then check key value is set then filter list by key
            if (filterByKey)
            {
                if (keyValuePair.Key.CheckNullOrEmpty())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.RequiredKey, keyValuePair.Key));

                keyValuePairs = keyValuePairs.FilterKeyValuePairListByKey(keyValuePair.Key);

                if (!keyValuePairs.Any())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, keyValuePair.Key));
            }

            if (keyValuePairs == null || !keyValuePairs.Any())
                return ApplicationConstants.ErrorMessages.EmptySet;

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
                return ApplicationConstants.ErrorMessages.EmptySet;

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
        private string Remove(List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toRemove, bool filterByMember = false)
        {
            if (toRemove.Key.CheckNullOrEmpty())
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);

            //filter by "key"
            var filteredKeyValuePairs = keyValuePairs.FilterKeyValuePairListByKey(toRemove.Key);

            if (!filteredKeyValuePairs.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, toRemove.Key));

            //filter by "member"
            if (filterByMember)
            {
                if (toRemove.Value.CheckNullOrEmpty())
                    throw new Exception(ApplicationConstants.ErrorMessages.RequiredMember);

                filteredKeyValuePairs = filteredKeyValuePairs.FilterKeyValuePairListByMember(toRemove.Value);

                if (!filteredKeyValuePairs.Any())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.MemberDoesNotExist, toRemove.Value));
            }

            //pass in the list returned for key and member to remove 
            if (keyValuePairs.RemoveItemsFromKeyValuePairList(filteredKeyValuePairs))
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