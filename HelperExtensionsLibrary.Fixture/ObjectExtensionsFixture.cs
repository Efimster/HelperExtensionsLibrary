using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Fixture.TestModels;
using HelperExtensionsLibrary.Objects;
using System.IO;



namespace HelperExtensionsLibrary.Fixture
{
    public class ObjectExtensionsFixture
    {
        [Fact]
        public void ToPropertyValuesDictionaryFixture()
        {
            var testObject = new ObjectWithProperties() { NotPrimitive = new NotPrimitiveClass()};
            var dictionary = testObject.ToPropertyValuesDictionary();

            ((int)dictionary["Property1"]).Should().Equal(1);
            ((string)dictionary["Property2"]).Should().Equal("string");
            ((DateTime)dictionary["Property3"]).Should().Equal(DateTime.MaxValue);
            ((NotPrimitiveClass)dictionary["NotPrimitive"]).Should().Not.Be.Null();

            dictionary = testObject.ToPropertyValuesDictionary(filter: descriptor => descriptor.PropertyType.IsPrimitive);
            dictionary.Should().Count.Exactly(1);
            dictionary.Should().Not.Contain.One(kv => kv.Key == "NotPrimitive");
        }

        [Fact]
        public void GetDefaultValueFixture()
        {
            //typeof(int).GetDefaultValue().Should().Equal(0);
            //typeof(string).GetDefaultValue().Should().Equal(null);
            //typeof(DateTime).GetDefaultValue().Should().Equal(DateTime.MinValue);

            
        }
        //[Serializable]
        private class TestClass// : IEquatable<TestClass>
        {
            public int _int;
            private List<string> _stringList;
            private KeyValuePair<string, int> _kvPair;

            public TestClass()
            {
            
            }

            public TestClass(int p_int, List<string> p_stringList, KeyValuePair<string, int> p_kvPair)
            {
                _int = p_int;
                _stringList = p_stringList;
                _kvPair = p_kvPair;
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as TestClass);
            }

            public bool Equals(TestClass other)
            {
                if (other == null)
                    return false;

                if (!_int.Equals(other._int) || !_kvPair.Equals(other._kvPair))
                    return false;

                if (_stringList == null)
                    return other._stringList == null;

                if (_stringList.Count != other._stringList.Count)
                    return false;

                for (int index = 0; index < _stringList.Count; index++)
                {
                    if (_stringList[index] == null)
                    {
                        if (other._stringList[index] != null)
                            return false;
                        continue;
                    }

                    if (!_stringList[index].Equals(other._stringList[index]))
                        return false;
                }
                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }


        }

        public class DataClass
        {
            public int IntField;
            public string StringField;
            public int[] ArrayField;
            string PrivateField;

            public string Private
            {
                get { return PrivateField; }
                set { PrivateField = value; }
            }

            public override string ToString()
            {
                return string.Format("IntField = {0};\r\nStrField = {1};\r\nAryField = {2} items;\r\nPrivate  = {3};",
                                      IntField, StringField, ArrayField == null ? 0 : ArrayField.Length, PrivateField);
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as DataClass);
            }

            public bool Equals(DataClass other)
            {
                if (other == null)
                    return false;
                
                return IntField.Equals(other.IntField) && StringField.Equals(other.StringField)
                    && PrivateField.Equals(other.PrivateField);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }


        [Fact]
        public void DeepCloneFixture()
        {
            
            var testObj = new TestClass(10, new List<string>{"fff",null,"vvv"}, new KeyValuePair<string,int>("key1",25));
            var data = new DataClass() { IntField = 5, ArrayField = new[] { 1, 2, 3 }, Private = "privateff", StringField = "strfield" };

            var cloneObj = testObj.DeepCopyByJSON();
            cloneObj.Should().Equal(testObj).Should().Not.Be.SameAs(testObj);
            var list = new List<object> { 1, 2, testObj, data };
            var list2 = new List<int> { 1,2,3,4};
            var list3 = new List<TestClass> { testObj, null, cloneObj };

            
            var clone = list.DeepCopyListByJSON();
            var clone2 = list2.DeepCopyByJSON();
            var clone3 = list3.DeepCopyByJSON();
    
            for (int index = 0; index < list.Count; index++)
            {
                list[index].Should().Equal(clone[index]);
                if (list[index]!=null && !list[index].GetType().IsValueType)
                    list[index].Should().Not.Be.SameAs(clone[index]);
            }
        }
    }
}
