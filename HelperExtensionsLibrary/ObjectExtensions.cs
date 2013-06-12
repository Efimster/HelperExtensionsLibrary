using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Objects
{
    /// <summary>
    /// Extensions for Object type
    /// </summary>
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class ObjectExtensions
    {
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
    }
}
