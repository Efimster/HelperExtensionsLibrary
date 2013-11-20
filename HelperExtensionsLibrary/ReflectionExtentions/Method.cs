using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Reflection
{
    public static class Method<T>
    {
        /// <summary>
        /// Returns delegate to return method name called by given delegate
        /// </summary>
        /// <typeparam name="TProperty">type of property</typeparam>
        /// <param name="selector">selector lambda expression tree</param>
        /// <returns>delegate</returns>
        public static Func<string> Nameof<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var expression = selector.Body as MethodCallExpression;
            if (expression == null)
            {
                throw new ArgumentException("'" + selector + "': is not a valid property or field selector");
            }

            return () => expression.Method.Name;
        }
        /// <summary>
        /// Returns property information of method called by given delegate
        /// </summary>
        /// <typeparam name="TReturn">given method return type</typeparam>
        /// <param name="selector">selector lambda expression tree</param>
        /// <returns>method information</returns>
        public static MethodInfo Infoof<TReturn>(Expression<Func<T, TReturn>> selector)
        {
            var expression = selector.Body as MethodCallExpression;
            if (expression == null)
            {
                throw new ArgumentException("'" + selector + "': is not a valid property or field selector");
            }

            return expression.Method as MethodInfo;
        }


    }
}
