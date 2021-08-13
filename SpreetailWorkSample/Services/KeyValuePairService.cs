using SpreetailWorkSample.Attributes;
using SpreetailWorkSample.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreetailWorkSample.Services
{
    public class KeyValuePairService<TKey, TMember>
    {
        public List<KeyValuePair<TKey, TMember>> KeyValuePairList = new List<KeyValuePair<TKey, TMember>>();
        public StringBuilder OutputBuilder = new StringBuilder();

        public void CommandSwitch(string command, TKey key, TMember member)
        {
            OutputBuilder.Clear();

            switch (command.ToUpper())
            {
                case ApplicationConstants.Commands.Keys:
                    GetKeyValuePairListKeysForDisplay();

                    //GetList(GetKeyValuePairListKeysForDisplay);
                    break;
                case ApplicationConstants.Commands.Members:
                    GetKeyValuePairListMembersForDisplay(key);
                    break;
                case ApplicationConstants.Commands.Add:
                    AddKeyValuePairToKeyValuePairList(key, member);
                    break;
                case ApplicationConstants.Commands.Remove:
                    //REMOVE foo bar
                    RemoveKeyValuePairFromList(key, member);
                    break;
                case ApplicationConstants.Commands.RemoveAll:
                    //REMOVEALL foo
                    RemoveKeyValuePairsForKeyFromList(key);
                    break;
                case ApplicationConstants.Commands.Clear:
                    ClearKeyValuePairList();
                    break;
                case ApplicationConstants.Commands.KeyExists:
                    GetKeyExistenceCheckResultForDisplay(key);
                    break;
                case ApplicationConstants.Commands.MemberExists:
                    GetMemberExistenceCheckResultByKeyForDisplay(key, member);
                    break;
                case ApplicationConstants.Commands.AllMembers:
                    GetKeyValuePairListMembersForDisplay(key);
                    break;
                case ApplicationConstants.Commands.Items:
                    GetKeyValuePairListItemsForDisplay();
                    break;
                case ApplicationConstants.Commands.Exit:
//                    OutputBuilder.Append(ApplicationConstants.SuccessMessages.ClosingApplication);

                    break;
                default:
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.InvalidCommand, command));
            }
        }

        //public List<TResult> GetList<TResult>(Func<List<TResult>> commandMethod)
        //{
        //    var result = commandMethod();

        //    return result;
        //}

        //public List<TResult> GetList<TArg1, TArg2, TResult>(TArg1 arg1, TArg2 arg2, Func<TArg1, TArg2, List<TResult>> commandMethod)
        //{
        //    var result = commandMethod(arg1, arg2);

        //    return result;
        //}

        #region Command Methods

        //KEYS
        private void GetKeyValuePairListKeysForDisplay()
        {
            if (KeyValuePairList == null || !KeyValuePairList.Any())
            {
                OutputBuilder.Append(ApplicationConstants.ErrorMessages.EmptySet);

                return;
            }

            var keys = (from kvp in KeyValuePairList select kvp.Key).Distinct().ToList();

            var displayList = new List<string>();

            var listNumber = 0;

            foreach (var key in keys)
            {
                listNumber++;

                OutputBuilder.AppendLine($"{listNumber}) {key}");
                //displayList.Add($"{listNumber}) {key}");
            }            
        }

        //[Command(ApplicationConstants.Commands.Keys)]
        //public List<string> GetKeyValuePairListKeysForDisplay()
        //{
        //    var displayList = new List<string>();

        //    var keys = GetList(GetKeyValuePairListKeys);

        //    if (keys == null || !keys.Any())
        //        return new List<string>();

        //    var listNumber = 0;
        //    foreach (var key in keys)
        //    {
        //        listNumber++;

        //        displayList.Add($"{listNumber}) {key}");
        //    }

        //    return displayList;
        //}

        //private List<TKey> GetKeyValuePairListKeys()
        //{
        //    if (KeyValuePairList == null || !KeyValuePairList.Any())
        //    {
        //        OutputBuilder.Append(ApplicationConstants.ErrorMessages.EmptySet);

        //        return new List<TKey>();
        //        //return new List<string> { ApplicationConstants.ErrorMessages.EmptySet };
        //    }

        //    return (from kvp in KeyValuePairList select kvp.Key).Distinct().ToList();
        //}

        //KEYEXISTS
        private void GetKeyExistenceCheckResultForDisplay(TKey key)
        {
            if (KeyValuePairListKeyExistenceCheck(key))
            {
                OutputBuilder.Append("true");
            }
            else
            {
                OutputBuilder.Append("false");
            }
        }

        //MEMBERS
        private void GetKeyValuePairListMembersForDisplay(TKey key)
        {
            var filteredList = new List<KeyValuePair<TKey, TMember>>();

            //if key is not null then check it exists in KeyValuePairList
            if (!key.CheckNullOrEmpty())
            {
                filteredList.AddRange(FilterKeyValuePairListByKey(key));

                if (!filteredList.Any())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));
            }
            else
            {
                //AllMembers, add all existing key value pairs
                filteredList.AddRange(KeyValuePairList);
            }

            if (filteredList == null || !filteredList.Any())
            {
                OutputBuilder.Append(ApplicationConstants.ErrorMessages.EmptySet);

                return;
            }

            var members = (from kvp in filteredList select kvp.Value);


            var listNumber = 0;
            foreach (var member in members)
            {
                listNumber++;

                OutputBuilder.AppendLine($"{listNumber}) {member}");
            }
        }

        //MEMBEREXISTS
        private void GetMemberExistenceCheckResultByKeyForDisplay(TKey key, TMember member)
        {
            if (KeyValuePairListMemberExistenceCheckByKey(key, member))
            {
                OutputBuilder.Append("true");
            }
            else
            {
                OutputBuilder.Append("false");
            }
        }

        //ADD
        private void AddKeyValuePairToKeyValuePairList(TKey key, TMember member)
        {
            if (key.CheckNullOrEmpty())
            {
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);
            }

            if (member.CheckNullOrEmpty())
            {
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredMember);
            }
            
            var keyValuePair = new KeyValuePair<TKey, TMember>(key.ToLowerForStringValue(), member.ToLowerForStringValue());

            if (KeyValuePairList.Contains(keyValuePair))
            {
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyMemberPairExists, member, key));
            }

            KeyValuePairList.Add(keyValuePair);

            OutputBuilder.Append(ApplicationConstants.SuccessMessages.Added);
        }

        //ITEMS
        private void GetKeyValuePairListItemsForDisplay()
        {
            if (KeyValuePairList == null || !KeyValuePairList.Any())
            {
                OutputBuilder.Append(ApplicationConstants.ErrorMessages.EmptySet);

                return;
            }

            var listNumber = 0;
            foreach (var kvp in KeyValuePairList)
            {
                listNumber++;

                OutputBuilder.AppendLine($"{listNumber}) {kvp.Key}: {kvp.Value}");
            }
        }

        //CLEAR
        private void ClearKeyValuePairList()
        {
            KeyValuePairList.Clear();

            OutputBuilder.Append(ApplicationConstants.SuccessMessages.Cleared);
        }

        //REMOVE foo bar
        private void RemoveKeyValuePairFromList(TKey key, TMember member)
        {
            if (key.CheckNullOrEmpty())
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);

            var filteredList = FilterKeyValuePairListByKey(key);

            if (!filteredList.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));

            filteredList = (from kvp in filteredList where kvp.Value.IsEqual(member) select kvp).ToList();

            if (!filteredList.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.MemberDoesNotExist, member));

            RemoveItemsFromKeyValuePairList(filteredList);

            OutputBuilder.Append(ApplicationConstants.SuccessMessages.Removed);
        }

        //REMOVEALL foo
        private void RemoveKeyValuePairsForKeyFromList(TKey key)
        {
            var filteredList = FilterKeyValuePairListByKey(key);

            if (!filteredList.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));

            RemoveItemsFromKeyValuePairList(filteredList);

            OutputBuilder.Append(ApplicationConstants.SuccessMessages.Removed);
        }
        #endregion

        #region Private Helper Methods
        private void RemoveItemsFromKeyValuePairList(List<KeyValuePair<TKey, TMember>> items)
        {
            var toRemoveCount = items.Count;

            for(var i = 0; i < toRemoveCount; i++)
            {
                KeyValuePairList.Remove(items[i]);
            }
        }

        private bool KeyValuePairListKeyExistenceCheck(TKey key)
        {
            var filteredList = FilterKeyValuePairListByKey(key);

            return filteredList.Any();
        }

        private bool KeyValuePairListMemberExistenceCheckByKey(TKey key, TMember member)
        {
            var filteredList = FilterKeyValuePairListByKey(key);

            return (from kvp in filteredList where kvp.Value.IsEqual(member) select kvp).Any();
        }

        //Use by commands: MEMBERS, KEYEXISTS
        private List<KeyValuePair<TKey, TMember>> FilterKeyValuePairListByKey(TKey key)
        {
            return (from kvp in KeyValuePairList where kvp.Key.IsEqual(key) select kvp).ToList();
        }

        #endregion
    }
}

namespace SpreetailWorkSample.Services.Mocks
{
    public class KeyValuePairServiceMock
    {
        private KeyValuePairService<string, string> _target;

        public KeyValuePairServiceMock()
        {
            _target = new KeyValuePairService<string, string>();           
        }

        //public void GetInputKeyValuePairMembersForDisplay(string command, string key, string value)
        //{
        //    _target.AddKeyValuePairToKeyValuePairList(key, value);
        //}       
    }
}