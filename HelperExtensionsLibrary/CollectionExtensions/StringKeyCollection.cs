using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Collections
{
    public class StringKeyCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IKeyedCollectionItem<string>
    {
    }
}
