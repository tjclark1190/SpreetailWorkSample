using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreetailWorkSample.Helpers;

namespace SpreetailWorkSample.Extensions
{
    public static class KeyValuePairExtensions
    {
        #region Command Methods

        //KEYS
        public static string GetKeyValuePairListKeysForDisplay<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValueList)
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
        public static string KeyExists<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key)
        {
            //keyValuePairs.FilterKeyValuePairListByKey(key);

            if (keyValuePairs.FilterKeyValuePairListByKey(key).Any())
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }

        //MEMBERS
        public static string GetKeyValuePairListMembersForDisplay<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key)
        {
            //if key is not null then check it exists in KeyValuePairList
            if (!key.CheckNullOrEmpty())
            {
                keyValuePairs = keyValuePairs.FilterKeyValuePairListByKey(key);

                if (!keyValuePairs.Any())
                    throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));
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
        public static string KeyValuePairExists<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey,TMember> keyValuePair)
        {
            //keyValuePairs.FilterKeyValuePairListByKey(key);

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
        public static string AddKeyValuePair<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
        {
            if (toAdd.Key.CheckNullOrEmpty())
            {
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);
            }

            if (toAdd.Value.CheckNullOrEmpty())
            {
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredMember);
            }
            
            if (keyValuePairs.Contains(toAdd))
            {
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyMemberPairExists, toAdd.Value, toAdd.Key));
            }

            keyValuePairs.Add(toAdd);

            return ApplicationConstants.SuccessMessages.Added;
        }

        //ITEMS
        public static string GetKeyValuePairListItemsForDisplay<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs)
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
        public static string ClearKeyValuePairList<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs)
        {
            keyValuePairs.Clear();

            return ApplicationConstants.SuccessMessages.Cleared;
        }

        //REMOVE foo bar
        public static string RemoveKeyValuePairFromList<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key, TMember member)
        {
            if (key.CheckNullOrEmpty())
                throw new Exception(ApplicationConstants.ErrorMessages.RequiredKey);

            //filter by "key"
            var keyValuePairsForKey = keyValuePairs.FilterKeyValuePairListByKey(key);

            if (!keyValuePairsForKey.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));

            //filter by "member"
            var keyValuePairsForMember = keyValuePairsForKey.FilterKeyValuePairListByMember(member); //(from kvp in keyValuePairsForKey where kvp.Value.IsEqual(member) select kvp).ToList();

            if (!keyValuePairsForMember.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.MemberDoesNotExist, member));

            //pass in the list returned for key and member to remove 
            keyValuePairs.RemoveItemsFromKeyValuePairList(keyValuePairsForMember);

            return ApplicationConstants.SuccessMessages.Removed;
        }

        //REMOVEALL foo
        public static string RemoveKeyValuePairsForKeyFromList<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key)
        {
            var keyValuePairsForKey = keyValuePairs.FilterKeyValuePairListByKey(key);

            if (!keyValuePairsForKey.Any())
                throw new Exception(string.Format(ApplicationConstants.ErrorMessages.KeyDoesNotExist, key));

            //pass in the list returned for key to remove 
            keyValuePairs.RemoveItemsFromKeyValuePairList(keyValuePairsForKey);

            return ApplicationConstants.SuccessMessages.Removed;
        }
        #endregion

        public static void RemoveItemsFromKeyValuePairList<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, List<KeyValuePair<TKey, TMember>> toRemove)
        {
            var toRemoveCount = toRemove.Count;

            for(var i = 0; i < toRemoveCount; i++)
            {
                keyValuePairs.Remove(toRemove[i]);
            }
        }

        //Use by commands: MEMBERS, KEYEXISTS
        public static List<KeyValuePair<TKey, TMember>> FilterKeyValuePairListByKey<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TKey key)
        {
            return (from kvp in keyValuePairs where kvp.Key.IsEqual(key) select kvp).ToList();
        }

        public static List<KeyValuePair<TKey, TMember>> FilterKeyValuePairListByMember<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, TMember member)
        {
            return (from kvp in keyValuePairs where kvp.Value.IsEqual(member) select kvp).ToList();
        }
    }
}

//namespace SpreetailWorkSample.Services.Mocks
//{
//    public class KeyValuePairServiceMock
//    {
//        private KeyValuePairService<string, string> _target;

//        public KeyValuePairServiceMock()
//        {
//            _target = new KeyValuePairService<string, string>();           
//        }

//        //public void GetInputKeyValuePairMembersForDisplay(string command, string key, string value)
//        //{
//        //    _target.AddKeyValuePairToKeyValuePairList(key, value);
//        //}       
//    }
//}