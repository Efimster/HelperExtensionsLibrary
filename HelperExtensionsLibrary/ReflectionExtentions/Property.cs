using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Reflection
{
    public static class Property<T>
    {
        /// <summary>
        /// Returns delegate to return property or field name accessed by given delegate
        /// </summary>
        /// <typeparam name="TProperty">type of property</typeparam>
        /// <param name="selector">selector lambda expression tree</param>
        /// <returns>delegate</returns>
        public static Func<string> Nameof<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var expression = selector.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("'" + selector + "': is not a valid property or field selector");
            }

            return () => expression.Member.Name;
        }
        /// <summary>
        /// Returns property information of property or field accessed by given delegate
        /// </summary>
        /// <typeparam name="TProperty">type of property</typeparam>
        /// <param name="selector">selector lambda expression tree</param>
        /// <returns>property information</returns>
        public static PropertyInfo Infoof<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var expression = selector.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("'" + selector + "': is not a valid property or field selector");
            }

            return expression.Member as PropertyInfo;
        }


    }
}
