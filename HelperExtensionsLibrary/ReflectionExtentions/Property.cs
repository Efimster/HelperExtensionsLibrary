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
        public static Func<string> Nameof<TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var expression = selector.Body as MemberExpression;
            if (expression == null)
            {
                throw new ArgumentException("'" + selector + "': is not a valid property or field selector");
            }

            return () => expression.Member.Name;
        }

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
