using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperExtensionsLibrary.Fixture.TestModels;
using Should.Fluent;
using Xunit;
using HelperExtensionsLibrary.Attributes;

namespace HelperExtensionsLibrary.Fixture
{
    public class AttributesExtensionsFixture
    {
        [Fact]
        public void FilterPropertiesByAttributeFixture()
        {
            var properties = typeof(ObjectWithProperties).GetProperties();
            properties.Should().Count.Exactly(4);
            properties.FilterPropertiesByAttribute<MyAttribute>()
                .Should().Count.Exactly(2);
            properties.FilterPropertiesByAttribute<MyAttribute, string>((attr, prop) => prop.Name == "Property2", (attr, prop) => prop.Name)
                .Should().Count.Exactly(1).First().Should().Equal("Property2");

        }
    }
}
