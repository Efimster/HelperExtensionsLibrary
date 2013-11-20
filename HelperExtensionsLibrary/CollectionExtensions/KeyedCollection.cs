using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HelperExtensionsLibrary.Collections
{
    /// <summary>
    /// Collection with items accessed by keys
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TItem">Item type</typeparam>
    public class KeyedCollection<TKey, TItem> : IEnumerable<TItem>, ICollection<TItem>, ISerializable, IDeserializationCallback
        where TItem : IKeyedCollectionItem<TKey>
    {
        Dictionary<TKey, TItem> dict;

        public KeyedCollection()
        {
            dict = new Dictionary<TKey, TItem>();
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            var baseEn = dict.GetEnumerator();

            try
            {
                while (baseEn.MoveNext())
                    yield return baseEn.Current.Value;
            }
            finally
            {
                baseEn.Dispose();
            }

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<TItem>)this).GetEnumerator();
        }

        /// <summary>
        /// Add item to collection
        /// </summary>
        /// <param name="item">item</param>
        public void Add(TItem item)
        {
            dict.Add(item.GetKey(), item);
        }
        /// <summary>
        /// Checks whether collection contains item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(TItem item)
        {
            return dict.ContainsKey(item.GetKey());
        }
        /// <summary>
        /// Checks whether collection contains item with given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return dict.ContainsKey(key);
        }


        public void CopyTo(TItem[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Clear collection data
        /// </summary>
        public void Clear()
        {
            dict.Clear();
        }
        /// <summary>
        /// Count of collection items
        /// </summary>
        public int Count
        {
            get { return dict.Count; }
        }
        /// <summary>
        /// Deletes item from collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(TItem item)
        {
            return dict.Remove(item.GetKey());
        }
        /// <summary>
        /// Returns collection item by key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>item</returns>
        public TItem this[TKey key]
        {
            get
            {
                TItem val;
                if (!dict.TryGetValue(key, out val))
                    return default(TItem);

                return val;
            }
            set
            {
                dict[key] = value;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)dict).GetObjectData(info, context);
        }

        public void OnDeserialization(object sender)
        {
            dict.OnDeserialization(sender);
        }
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return dict.ToString();
        }
    }
}
