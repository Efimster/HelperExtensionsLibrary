using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Dictioinaries;
using HelperExtensionsLibrary.IEnumerable;

namespace HelperExtensionsLibrary.Fixture
{
    public class DictionaryExtensionsFixture
    {
        [Fact]
        public void TryGetValueFixture()
        {
            var dict = new Dictionary<string, int>() { { "a",1 }, { "b",2 } };
            dict.TryGetValue("b").Should().Equal(2);
            dict.TryGetValue("c").Should().Be.Null();
            (dict.TryGetValue("c") ?? 3).Should().Equal(3); 
        }

        [Fact]
        public void ToDictionaryFixture()
        {
            var dict = new Dictionary<string, int>() { { "a", 1 }, { "b", 2 } };

            dict.ToList().ToDictionary().ForEach(item => item.Value.Should().Equal(dict[item.Key]));
        }
    }
}
