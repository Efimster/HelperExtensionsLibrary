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
        /// <summary>
        /// Convenient version of TryGetValue method for dictionaries with value type key
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dict">Dictionary</param>
        /// <param name="key">Key</param>
        /// <returns>value</returns>
        public static TValue? TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) where TValue : struct
        {
            TValue val;
            if (!dict.TryGetValue(key, out val))
                return null;

            return val;
        }
        /// <summary>
        /// Enumerable To Dictionary conversion
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="coll">enumerable collection</param>
        /// <returns>dictionary</returns>
        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> coll)
        {
            return coll.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
