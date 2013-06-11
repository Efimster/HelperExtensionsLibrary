using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.IEnumerable
{
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// iterates over collection and aplies action on every item
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="list">iterative collection</param>
        /// <param name="modifier">action</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> modifier)
        {
            if (list == null)
                return;

            foreach (var item in list)
                modifier(item);
        }

        /// <summary>
        /// iterates over collection and aplies function on each item while returns true
        /// </summary>
        /// <typeparam name="T">Type of collection items</typeparam>
        /// <param name="list">iteretive collection</param>
        /// <param name="modifier">function applied to each element (with element index).  Iteration interrapted, once return false </param>
        public static void ForEach<T>(this IEnumerable<T> list, Func<T, int, bool> modifier)
        {
            if (list == null)
                return;

            int idx = 0;

            foreach (var item in list)
                if (!modifier(item, idx++))
                    return;

        }
        /// <summary>
        /// Converts one collection to another one by applying conversion function
        /// </summary>
        /// <typeparam name="T1">Type of input collection items</typeparam>
        /// <typeparam name="T2">Type of output item collection</typeparam>
        /// <param name="list">iterative collection</param>
        /// <param name="modifier">converting function</param>
        /// <returns></returns>
        public static IEnumerable<T2> Convert<T1, T2>(this IEnumerable<T1> list, Func<T1, T2> modifier)
        {
            if (list == null)
                yield break;

            foreach (var item in list)
                yield return modifier(item);
        }
        /// <summary>
        /// Merge two collections by applying selector function to elements with same index
        /// </summary>
        /// <typeparam name="TLeft">Type of input collection items</typeparam>
        /// <typeparam name="TRight">Type of output item collection</typeparam>
        /// <typeparam name="TResult">Type of result collection</typeparam>
        /// <param name="left">merging iterative collection</param>
        /// <param name="right">second iterative collection to merge</param>
        /// <param name="selector">function takes two appropriate items and produce one resulting items</param>
        /// <returns></returns>
        public static IEnumerable<TResult> JoinByIndex<TLeft, TRight, TResult>(this IEnumerable<TLeft> left, 
            IEnumerable<TRight> right, 
            Func<TLeft, TRight, TResult> selector)
        {
            if (left == null || right == null)
                yield break;

            var ltor = left.GetEnumerator();
            var rtor = right.GetEnumerator();

            try
            {
                while (ltor.MoveNext() & rtor.MoveNext())
                    yield return selector(ltor.Current, rtor.Current);
            }
            finally
            {
                ltor.Dispose();
                rtor.Dispose();
            }
        }

        /// <summary>
        /// Checks whether two itarative collections have identical items (include items count and places)
        /// </summary>
        /// <typeparam name="T">items type</typeparam>
        /// <param name="left">left collection</param>
        /// <param name="right">right collection</param>
        /// <returns>true - if collections are equel</returns>
        public static bool EquelsByIndex<T>(this IEnumerable<T> left, IEnumerable<T> right) where T : IEquatable<T>
        {
            if (left == null || right == null)
                return true;

            var ltor = left.GetEnumerator();
            var rtor = right.GetEnumerator();

            try
            {
                bool lp = false, rp = false;
                while ((lp = ltor.MoveNext()) & (rp = rtor.MoveNext()))
                    if (!ltor.Current.Equals(rtor.Current))
                        return false;

                return lp == rp;
            }
            finally
            {
                ltor.Dispose();
                rtor.Dispose();
            }
        }

        /// <summary>
        /// Iterates over collection to element with given index. Returns element or default value if index is out of bounds 
        /// </summary>
        /// <typeparam name="T">Elemnts type</typeparam>
        /// <param name="list">list of elements</param>
        /// <param name="index">index</param>
        /// <param name="defaulValue">default value</param>
        /// <returns>element</returns>
        public static T ElementAtOrDefault<T>(this IEnumerable<T> list, int index, T defaulValue)
        {
            int currIdx = 0;
            foreach (var item in list)
            {
                if (currIdx++ == index)
                    return item;
            }

            return defaulValue;
        }
        /// <summary>
        /// Returns index of first element that equals to the input one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">iterative collection</param>
        /// <param name="item">finding element</param>
        /// <returns>element index</returns>
        public static int FirstIndexEquels<T>(this IEnumerable<T> list, T item) where T : IEquatable<T>
        {
            int i = 0;
            foreach (var x in list)
            {
                if (x.Equals(item))
                    return i;

                i++;
            }

            return -1;

        }
    }
}
