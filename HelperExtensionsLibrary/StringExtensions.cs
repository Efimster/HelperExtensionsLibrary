using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperExtensionsLibrary.IEnumerable;

namespace HelperExtensionsLibrary.Strings
{
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class StringExtensions
    {
        
        /// <summary>
        /// Splits string to iterative collection by delimiter
        /// </summary>
        /// <param name="str">splited string</param>
        /// <param name="delimiter">delimiter</param>
        /// <returns>string iterative collection</returns>
        public static IEnumerable<string> SplitExt(this string str, char delimiter)
        {
            if (string.IsNullOrEmpty(str))
                yield break;

            int idx, startIdx = 0;
            do
            {
                idx = str.IndexOf(delimiter, startIdx);

                int len = idx >= 0 ? idx - startIdx : str.Length - startIdx;

                if (len > 0)
                    yield return str.Substring(startIdx, len);

                startIdx = idx + 1;
            } while (idx >= 0);
        }
                
        /// <summary>
        /// Split string item by delimiter
        /// </summary>
        /// <param name="list">item collections</param>
        /// <param name="delimiter">delimiter</param>
        /// <returns>string iterative collection</returns>
        public static IEnumerable<string> SplitExt(this IEnumerable<string> list, char delimiter)
        {
            if (list == null)
                yield break;

            foreach (var str in list)
                foreach (var item in str.SplitExt(delimiter))
                    yield return item;
        }
        /// <summary>
        /// Concatenates collection elemens to string using delimiter
        /// </summary>
        /// <typeparam name="T">collection items type</typeparam>
        /// <param name="list">collection</param>
        /// <param name="delimiter">delimiter</param>
        /// <returns>joined string</returns>
        public static string Join2String<T>(this IEnumerable<T> list, char delimiter)
        {
            return Join2String<T>(list, delimiter.ToString());
        }

        /// <summary>
        /// Concatenates collection elemens to string using delimiter
        /// </summary>
        /// <typeparam name="T">collection items type</typeparam>
        /// <param name="list">collection</param>
        /// <param name="delimiter">delimiter</param>
        /// <returns>joined string</returns>
        public static string Join2String<T>(this IEnumerable<T> list, string delimiter)
        {
            if (list == null)
                return string.Empty;


            var sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString());
                sb.Append(delimiter);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        /// <summary>
        /// string.IsNullOrEmpty(value) counterpart
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>true -  IsNullOrEmpty</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Splits string to iterative collection by delimiter
        /// </summary>
        /// <param name="str">splited string</param>
        /// <param name="delimiter">delimiter</param>
        /// <param name="trim">true : trim values</param>
        /// <returns>string iterative collection</returns>
        public static IEnumerable<string> SplitExt(this string str, string delimiter, bool trim = true)
        {
            if (string.IsNullOrEmpty(str))
                yield break;

            int idx, startIdx = 0;
            do
            {
                idx = str.IndexOf(delimiter, startIdx);

                int len = idx >= 0 ? idx - startIdx : str.Length - startIdx;

                if (len > 0)
                {
                    yield return trim ? str.Substring(startIdx, len).Trim() : str.Substring(startIdx, len);
                }

                startIdx = idx + delimiter.Length;
            } while (idx >= 0);
        }

        /// <summary>
        /// Split string to dictionary
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary key</typeparam>
        /// <typeparam name="TVal">Type of dictionary value</typeparam>
        /// <param name="str">string to split</param>
        /// <param name="entryDelimiter">delimiter of dictionary entries</param>
        /// <param name="keyValueDelimiter">delmiter of key|value</param>
        /// <param name="keySelector">key selector</param>
        /// <param name="valueSelector">value selector</param>
        /// <param name="trim">trim keys and value</param>
        /// <returns>dictionary</returns>
        public static IDictionary<TKey, TVal> SplitToDictionaryExt<TKey, TVal>(this string str,
            string entryDelimiter,
            string keyValueDelimiter,
            Func<string, TKey> keySelector,
            Func<string, TVal> valueSelector,
            bool trim = true
            )
        {
            IDictionary<TKey, TVal> result = new Dictionary<TKey, TVal>();

            str.SplitExt(entryDelimiter, trim).ForEach(entry =>
            {
                var keyValue = entry.SplitExt(keyValueDelimiter, trim).ToArray();
                result[keySelector(keyValue[0])] = valueSelector(keyValue[1]);
            });

            return result;
        }

        /// <summary>
        /// Split string to dictionary
        /// </summary>
        /// <typeparam name="TVal">Type of dictionary value</typeparam>
        /// <param name="str">string to split</param>
        /// <param name="entryDelimiter">delimiter of dictionary entries</param>
        /// <param name="keyValueDelimiter">delmiter of key|value</param>
        /// <param name="valueSelector">value selector</param>
        /// <param name="trim">trim keys and value</param>
        /// <returns>dictionary</returns>
        public static IDictionary<string, TVal> SplitToDictionaryExt<TVal>(this string str,
            string entryDelimiter,
            string keyValueDelimiter,
            Func<string, TVal> valueSelector,
            bool trim = true
            )
        {
            return str.SplitToDictionaryExt(entryDelimiter, keyValueDelimiter, x => x, valueSelector, trim);
        }

        /// <summary>
        /// Split string to dictionary
        /// </summary>
        /// <param name="str">string to split</param>
        /// <param name="entryDelimiter">delimiter of dictionary entries</param>
        /// <param name="keyValueDelimiter">delmiter of key|value</param>
        /// <param name="trim">trim keys and value</param>
        /// <returns>dictionary</returns>
        public static IDictionary<string, string> SplitToDictionaryExt(this string str,
            string entryDelimiter,
            string keyValueDelimiter,
            bool trim = true
            )
        {
            return str.SplitToDictionaryExt(entryDelimiter, keyValueDelimiter, x => x, x => x, trim);
        }
    }
}
