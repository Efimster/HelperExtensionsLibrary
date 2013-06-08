using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Collections
{
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class CollectionExtentions
    {
        /// <summary>
        /// Checks whether collection equels to NULL or contains no items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
