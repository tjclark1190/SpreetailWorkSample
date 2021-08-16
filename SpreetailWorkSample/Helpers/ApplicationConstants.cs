using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SpreetailWorkSample.Helpers
{
    public static class ApplicationConstants
    {
        //public const string Delimiter = " ";

        public static class Commands
        {
            public const string Exit = "EXIT";

            /// <summary>
            /// Keys
            /// Returns all the keys in the dictionary.  Order is not guaranteed.

            ///Example
            ///```
            ///> ADD foo bar
            ///) Added
            ///> ADD baz bang
            ///) Added
            ///> KEYS
            ///1) foo
            ///2) baz
            ///```
            /// </summary>
            public const string Keys = "KEYS";

            /// <summary>
            /// Members
            /// Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if the key does not exists.

            ///Example:
            ///```
            ///> ADD foo bar
            ///> ADD foo baz
            ///> MEMBERS foo
            ///1) bar
            ///2) baz

            ///> MEMBERS bad
            ///) ERROR, key does not exist.
            ///```
            /// </summary>
            public const string Members = "MEMBERS";

            /// <summary>
            /// Add
            /// Adds a member to a collection for a given key. Displays an error if the member already exists for the key.

            ///```
            ///> ADD foo bar
            ///) Added
            ///> ADD foo baz
            ///) Added
            ///> ADD foo bar
            ///) ERROR, member already exists for key
            ///```
            /// </summary>
            public const string Add = "ADD";

            /// <summary>
            /// Remove
            /// Removes a member from a key.  If the last member is removed from the key, the key is removed from the dictionary. If the key or member does not exist, displays an error.

            ///Example:
            ///```
            ///> ADD foo bar
            ///) Added
            ///> ADD foo baz
            ///) Added

            ///> REMOVE foo bar
            ///) Removed
            ///> REMOVE foo bar
            ///) ERROR, member does not exist

            ///> KEYS
            ///1) foo

            ///> REMOVE foo baz
            ///) Removed

            ///> KEYS
            ///) empty set

            ///> REMOVE boom pow
            ///) ERROR, key does not exist
            ///```

            /// </summary>
            public const string Remove = "REMOVE";

            /// <summary>
            /// RemoveAll
            /// Removes all members for a key and removes the key from the dictionary. Returns an error if the key does not exist.

            ///Example:
            ///```
            ///> ADD foo bar
            ///) Added
            ///> ADD foo baz
            ///) Added
            ///> KEYS
            ///1) foo

            ///> REMOVEALL foo
            ///) Removed

            ///> KEYS
            //(empty set)

            ///REMOVEALL foo
            ///) ERROR, key does not exist

            ///```
            /// </summary>
            public const string RemoveAll = "REMOVEALL";

            /// <summary>
            /// Clear
            /// Removes all keys and all members from the dictionary.

            ///Example:
            ///```
            ///> ADD foo bar
            ///) Added
            ///> ADD bang zip
            ///) Added
            ///> KEYS
            ///1) foo
            ///2) bang

            ///> CLEAR
            ///) Cleared

            ///> KEYS
            ///(empty set)

            ///> CLEAR
            ///) Cleared

            ///> KEYS
            ///(empty set)

            ///```
            /// </summary>
            public const string Clear = "CLEAR";

            /// <summary>
            /// KEYEXISTS
            /// Returns whether a key exists or not.

            ///Example:
            ///```
            ///> KEYEXISTS foo
            ///) false
            ///> ADD foo bar
            ///) Added
            ///> KEYEXISTS foo
            ///) true
            ///```
            /// </summary>
            public const string KeyExists = "KEYEXISTS";

            /// <summary>
            /// MEMBEREXISTS
            /// Returns whether a member exists within a key.Returns false if the key does not exist.

            /// Example:
            /// ```
            /// > MEMBEREXISTS foo bar
            /// ) false
            /// > ADD foo bar
            /// ) Added
            /// > MEMBEREXISTS foo bar
            /// ) true
            /// > MEMBEREXISTS foo baz
            /// ) false

            /// </summary>
            public const string MemberExists = "MEMBEREXISTS";

            /// <summary>
            /// ALLMEMBERS
            /// Returns all the members in the dictionary.Returns nothing if there are none.Order is not guaranteed.


            /// Example:
            /// ```
            /// > ALLMEMBERS
            /// (empty set)
            /// > ADD foo bar
            /// ) Added
            /// > ADD foo baz
            /// ) Added
            /// > ALLMEMBERS
            /// 1) bar
            /// 2) baz
            /// > ADD bang bar
            /// ) Added
            /// > ADD bang baz
            /// > ALLMEMBERS
            /// 1) bar
            /// 2) baz
            /// 3) bar
            /// 4) baz
            /// ```
            /// </summary>
            public const string AllMembers = "ALLMEMBERS";

            /// <summary>
            /// ITEMS
            /// Returns all keys in the dictionary and all of their members.Returns nothing if there are none.Order is not guaranteed.

            /// Example:
            /// ```
            /// > ITEMS
            /// (empty set)
            /// > ADD foo bar
            /// ) Added
            /// > ADD foo baz
            /// ) Added
            /// > ITEMS
            /// 1) foo: bar
            /// 2) foo: baz
            /// > ADD bang bar
            /// ) Added
            ///  ADD bang baz
            /// > ITEMS
            /// 1) foo: bar
            /// 2) foo: baz
            /// 3) bang: bar
            /// 4) bang: baz
            /// ```
            /// </summary>
            public const string Items = "ITEMS";
        }

        public static class ErrorMessages
        {
            public static readonly string RequiredCommand = "Command is required";
            public static readonly string RequiredKey = "Key is required";
            public static readonly string RequiredMember = "Member is required";
            public static readonly string KeyDoesNotExist = "key \"{0}\" does not exist";
            public static readonly string MemberDoesNotExist = "member \"{0}\" does not exist";
            public static readonly string KeyMemberPairExists = "Member \"{0}\" already exists for key \"{1}\"";
            public static readonly string InvalidCommand = "\"{0}\" is not a command";
            public static readonly string EmptySet = "(empty set)";
            public static readonly string BlankOutputString = "Failed to generate output string";
            public static readonly string BlankInputArray = "InputArray does not have any elements";
            public static readonly string RemoveFailed = "Data was not removed";
            public static readonly string AddFailed = "Data was not added";
            public static readonly string UserInputRequired  = "User input is required";            
        }

        public static class SuccessMessages
        {
            public static readonly string Added = "Added";
            public static readonly string Removed = "Removed";
            public static readonly string Cleared = "Cleared";
            public static readonly string ClosingApplication = "Closing Application";
            public static readonly string True = "true";
            public static readonly string False = "false";
        }
    }
}
