﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Attributes
{
    /// <summary>
    /// Extensions for Object type
    /// </summary>
    [DebuggerStepThrough, DebuggerNonUserCode]
    public static class AttributesExtensions
    {
        
        /// <summary>
        /// Filter properties by assigned attribute
        /// </summary>
        /// <typeparam name="TAttr">Attribute</typeparam>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="properties">Properties collection</param>
        /// <param name="filter">filtering function</param>
        /// <param name="selector">converting function</param>
        /// <param name="inferHelper"></param>
        /// <returns>filtered list</returns>
        public static IEnumerable<TRes> FilterPropertiesByAttribute<TAttr, TRes>(this ICollection<PropertyInfo> properties,
        Func<TAttr, PropertyInfo, bool> filter,
        Func<TAttr, PropertyInfo, TRes> selector,
        TAttr inferHelper = default(TAttr))
        where TAttr : Attribute
            {
                IList<TRes> attrs = new List<TRes>(1);
                foreach (var prop in properties)
                {
                    var x = prop.GetCustomAttributes<TAttr>(false).FirstOrDefault();
                    if (x != null)
                    {
                        if (filter == null || filter(x, prop))
                        {
                            yield return selector(x, prop);
                        }
                    }
                }

            }

        /// <summary>
        /// Filter properties by assigned attribute
        /// </summary>
        /// <typeparam name="TAttr">Attribute</typeparam>
        /// <param name="properties">Properties collection</param>
        /// <param name="filter">filtering function</param>
        /// <returns>filtered list</returns>
        public static IEnumerable<PropertyInfo> FilterPropertiesByAttribute<TAttr>(this ICollection<PropertyInfo> properties,
            Func<TAttr, PropertyInfo, bool> filter = null)
            where TAttr : Attribute
        {
            return FilterPropertiesByAttribute<TAttr, PropertyInfo>(properties, filter, (x, y) => y);
        }
    }
}
