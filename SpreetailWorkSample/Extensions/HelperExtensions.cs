using System;
using System.Collections.Generic;
using System.Text;

namespace SpreetailWorkSample.Extensions
{
    public static class HelperExtensions
    {
        public static T ToLowerForStringValue<T>(this T value)
        {
            if (typeof(T) == typeof(string))
            {
                var loweredValue = (value as string).ToLower();

                return (T)Convert.ChangeType(loweredValue, typeof(T));
            }

            return value;
        }

        public static bool IsEqual<T>(this T valueOne, T valueTwo)
        {
            if (typeof(T) == typeof(string))
                return (valueOne as string).Equals(valueTwo as string, StringComparison.InvariantCultureIgnoreCase);

            return valueOne.Equals(valueTwo);
        }

        public static bool CheckNullOrEmpty<T>(this T value)
        {
            if (typeof(T) == typeof(string))
                return string.IsNullOrEmpty(value as string);

            return value == null || value.Equals(default(T));
        }
    }

}
