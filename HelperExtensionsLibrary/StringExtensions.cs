using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns></returns>
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
                
        
        public static IEnumerable<string> SplitExt(this IEnumerable<string> list, char delimiter)
        {
            if (list == null)
                yield break;

            foreach (var str in list)
                foreach (var item in str.SplitExt(delimiter))
                    yield return item;
        }

        public static string Join2String<T>(this IEnumerable<T> list, char delimiter)
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
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
