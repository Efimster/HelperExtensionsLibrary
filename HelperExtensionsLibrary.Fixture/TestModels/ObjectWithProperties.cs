using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Fixture.TestModels
{
    public class ObjectWithProperties
    {

        public int Property1 { get { return 1; } }
        [MyAttribute]
        public string Property2 { get { return "string"; } }
        [MyAttribute]
        public DateTime? Property3 { get { return DateTime.MaxValue; } }
        public int Property4 { get; set; }

        public NotPrimitiveClass NotPrimitive { get; set; }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class MyAttribute : Attribute
    {
    }

    public class NotPrimitiveClass
    {
        public int Property1 { get { return 1; } }
        [MyAttribute]
        public string Property2 { get { return "string"; } }
    }
}
