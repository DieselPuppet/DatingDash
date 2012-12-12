using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelData.Utility.Extensions
{
    public static class StringExtensions
    {
        public const string DefaultSeparator = ", ";

        public static string Join(this IEnumerable<string> value, string separator = DefaultSeparator,
            string prefix = null, string postfix = null)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            string result = String.Join(separator, value.ToArray());

            if (value.Count() > 0 && (!String.IsNullOrEmpty(prefix) || !String.IsNullOrEmpty(postfix)))
            {
                prefix = prefix ?? String.Empty;
                postfix = postfix ?? String.Empty;

                result = String.Concat(prefix, result, postfix);
            }

            return result;
        }

        /*
    #if !UNITY_IPHONE
        /// <remarks>Метод не работает под IOS (и возможно под Android), выдаёт ошибку ExecutionEngineException: Attempting to JIT compile method in AOT only.</remarks>
        public static string JoinToString<T>(this IEnumerable<T> value, string separator = DefaultSeparator,
            Func<T, string> toStringConverter = null,
            string prefix = null, string postfix = null)
        {
            // не перегружаем Join для строки (см. выше), поскольку простой вызов Join(separator) будет в этом случае Ambiguous

            if (toStringConverter == null)
                toStringConverter = (T val) => val.ToString();

            return Join(value.Select(v => toStringConverter(v)), separator, prefix, postfix);
        }
    #endif
        */

        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return value.IsNullOrEmpty() || value.Trim().Length == 0;
        }
    }
}