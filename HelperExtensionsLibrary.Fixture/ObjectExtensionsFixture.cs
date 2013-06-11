using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Fixture.TestModels;
using HelperExtensionsLibrary.Objects;

namespace HelperExtensionsLibrary.Fixture
{
    public class ObjectExtensionsFixture
    {
        [Fact]
        public void ToPropertyValuesDictionaryFixture()
        {
            var testObject = new ObjectWithProperties();
            var dictionary = testObject.ToPropertyValuesDictionary();

            ((int)dictionary["Property1"]).Should().Equal(1);
            ((string)dictionary["Property2"]).Should().Equal("string");
            ((DateTime)dictionary["Property3"]).Should().Equal(DateTime.MaxValue);
        }

        [Fact]
        public void GetDefaultValueFixture()
        {
            typeof(int).GetDefaultValue().Should().Equal(0);
            typeof(string).GetDefaultValue().Should().Equal(null);
            typeof(DateTime).GetDefaultValue().Should().Equal(DateTime.MinValue);
        }
    }
}
