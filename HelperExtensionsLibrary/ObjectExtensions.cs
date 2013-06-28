using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HelperExtensionsLibrary.Objects
{
    /// <summary>
    /// Extensions for Object type
    /// </summary>
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class ObjectExtensions
    {
        static Newtonsoft.Json.JsonSerializerSettings jsonDeepCopySettings;
        
        static ObjectExtensions()
        {
            jsonDeepCopySettings = new Newtonsoft.Json.JsonSerializerSettings();
            Newtonsoft.Json.Serialization.DefaultContractResolver dcr = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            dcr.DefaultMembersSearchFlags |= System.Reflection.BindingFlags.NonPublic;
            jsonDeepCopySettings.ContractResolver = dcr;
        }
        
        
        /// <summary>
        /// Convert Object to dictionary of it properties
        /// </summary>
        /// <param name="values">object with properties</param>
        /// <param name="filterDefaultValues">True - filter properties with defult values assigned</param>
        /// <returns>dictionary of properties</returns>
        public static IDictionary<string, dynamic> ToPropertyValuesDictionary(this object values, bool filterDefaultValues = true, Func<PropertyDescriptor, bool> filter = null)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (values != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
                {
                    if (filter != null && !filter(descriptor))
                    {
                        continue;
                    }

                    object obj2 = descriptor.GetValue(values); 
                    if (filterDefaultValues && (obj2 == null || obj2.Equals(descriptor.PropertyType.GetDefaultValue())))
                        continue;

                    dictionary.Add(descriptor.Name, obj2);
                }
            }

            return dictionary;
        }

        /// <summary>
        /// Get default value of the Type
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>defualt value of Type</returns>
        public static object GetDefaultValue(this Type t)
        {
            if (t.IsValueType && Nullable.GetUnderlyingType(t) == null)
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Creates object deep copy
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object</param>
        /// <returns>object clone</returns>
        public static T DeepCopy<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// Creates object deep copy
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">object</param>
        /// <returns>object clone</returns>
        public static T DeepCopyByJSON<T>(this T obj) where T : class
        {
            var serializedObjectString = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.None,  jsonDeepCopySettings);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(serializedObjectString, jsonDeepCopySettings);
        }

        public static IList<object> DeepCopyListByJSON(this IList<object> list)
        {
            var clone = new List<object>(list.Count);
            for (int index = 0; index < list.Count; index++)
            {
                var type = list[index].GetType();
                if (list[index].GetType().IsValueType)
                {
                    clone.Add(list[index]);
                    continue;
                }

                var serializedObjectString = Newtonsoft.Json.JsonConvert.SerializeObject(list[index], Formatting.None, jsonDeepCopySettings);
                var cloneObject = Newtonsoft.Json.JsonConvert.DeserializeObject(serializedObjectString, type, jsonDeepCopySettings);
                clone.Add(cloneObject);
            }

            return clone;
        }

    }
}
