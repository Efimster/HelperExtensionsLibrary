using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HelperExtensionsLibrary.Collections
{
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


        public void Add(TItem item)
        {
            dict.Add(item.GetKey(), item);
        }

        public bool Contains(TItem item)
        {
            return dict.ContainsKey(item.GetKey());
        }

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


        public void Clear()
        {
            dict.Clear();
        }

        public int Count
        {
            get { return dict.Count; }
        }

        public bool Remove(TItem item)
        {
            return dict.Remove(item.GetKey());
        }

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

        public override string ToString()
        {
            return dict.ToString();
        }
    }
}
