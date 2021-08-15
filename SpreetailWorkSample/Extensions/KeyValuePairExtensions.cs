using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreetailWorkSample.Helpers;

namespace SpreetailWorkSample.Extensions
{
    public static class KeyValuePairExtensions
    {
        public static bool AddKeyValuePair<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, KeyValuePair<TKey, TMember> toAdd)
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

            return keyValuePairs.Contains(toAdd);
        }

        public static bool RemoveItemsFromKeyValuePairList<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, List<KeyValuePair<TKey, TMember>> toRemove)
        {
            var toRemoveCount = toRemove.Count;

            for(var i = 0; i < toRemoveCount; i++)
            {
                keyValuePairs.Remove(toRemove[i]);
            }

            //check if the key value pairs were removed
            return keyValuePairs.CheckIfItemsRemoved(toRemove);
        }

        public static bool CheckIfItemsRemoved<TKey, TMember>(this List<KeyValuePair<TKey, TMember>> keyValuePairs, List<KeyValuePair<TKey, TMember>> toRemove)
        {
            var isRemoved = true;

            foreach (var tr in toRemove)
            {
                if (!keyValuePairs.Contains(tr))
                    continue;

                isRemoved = false;

                break;
            }

            return isRemoved;
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