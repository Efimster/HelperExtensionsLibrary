using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Dictioinaries
{
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class DictionaryExtensions
    {
        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : struct
        {
            TValue val;
            if (!dict.TryGetValue(key, out val))
                return null;

            return val;
        }
        /// <summary>
        /// To Dictionary
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> coll)
        {
            return coll.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
