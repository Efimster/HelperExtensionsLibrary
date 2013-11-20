using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Collections
{
    /// <summary>
    /// Collection with items accessed by string keys
    /// </summary>
    /// <typeparam name="TItem">Item type</typeparam>
    public class StringKeyCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IKeyedCollectionItem<string>
    {
    }
}
