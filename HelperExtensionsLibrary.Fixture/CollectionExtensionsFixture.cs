using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Collections;
using HelperExtensionsLibrary.IEnumerable;

namespace HelperExtensionsLibrary.Fixture
{
    public class CollectionExtensionsFixture
    {
        [Fact]
        public void IsEmptyFixture()
        {
            ICollection<int> collection = null; 
            collection.IsEmpty().Should().Be.True();
            collection = new List<int>();
            collection.IsEmpty().Should().Be.True();
            collection.Add(1);
            collection.IsEmpty().Should().Be.False();
        }

        public class Item : IKeyedCollectionItem<string>
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public string GetKey()
            {
                return Name;
            }
        }
        [Fact]
        public void KeyedCoolectionFixture()
        {
            var collection = new StringKeyCollection<Item>();
            collection.Add(new Item() {Name = "n1", Value = 1});
            collection.Add(new Item() { Name = "n2", Value = 2 });
            collection["n1"].Value.Should().Equal(1);
            collection.ContainsKey("n2").Should().Be.True();
        }
    }
}
