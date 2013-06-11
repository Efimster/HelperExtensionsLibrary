using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should.Fluent;
using Xunit;
using HelperExtensionsLibrary.Reflection;
using HelperExtensionsLibrary.Fixture.TestModels;

namespace HelperExtensionsLibrary.Fixture
{
    public class ReflectionExtensionsFixture
    {
        [Fact]
        public void ConstructFieldOrPropertyGetterFixture()
        {
            var func = ReflectionExtensions.ConstructFieldOrPropertyGetter<ObjectWithProperties, int>("Property1");
            func(new ObjectWithProperties()).Should().Equal(1);

            var delegate1 = ReflectionExtensions.ConstructFieldOrPropertyGetter(typeof(ObjectWithProperties), "Property1");
            delegate1.DynamicInvoke(new ObjectWithProperties()).Should().Equal(1);

        }

        [Fact]
        public void ConstructFieldOrPropertySetterFixture()
        {
            var action = ReflectionExtensions.ConstructFieldOrPropertySetter<ObjectWithProperties, int>("Property4");
            var obj = new ObjectWithProperties();
            obj.Property4 = 5;
            action(obj,2);
            obj.Property4.Should().Equal(2);

        }
    }
}
