using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HelperExtensionsLibrary.Collections;
using HelperExtensionsLibrary.Strings;

namespace HelperExtensionsLibrary.Reflection
{
    /// <summary>
    /// Extentions for reflection
    /// </summary>
    public static class ReflectionExtenbtions
    {
        /// <summary>
        /// Get Action for field or property setting
        /// </summary>
        /// <typeparam name="TObj">Field or property owner type</typeparam>
        /// <typeparam name="TVal">value type</typeparam>
        /// <param name="fieldOrPropertyName">field or property name</param>
        /// <returns>Action(owner,value)</returns>
        public static Action<TObj, TVal> ConstructFieldOrPropertySetter<TObj, TVal>(string fieldOrPropertyName)
        {
            ParameterExpression param1 = Expression.Parameter(typeof(TObj), "TObj");
            MemberExpression property = Expression.PropertyOrField(param1, fieldOrPropertyName);
            ParameterExpression param2 = Expression.Parameter(typeof(TVal), "TVal");

            BinaryExpression ass = Expression.Assign(property, param2);
            Action<TObj, TVal> setter = Expression.Lambda<Action<TObj, TVal>>(ass, param1, param2).Compile();
            return setter;

        }
        /// <summary>
        /// Construct Delegate to get field or property value
        /// </summary>
        /// <typeparam name="TObj">property owner type</typeparam>
        /// <param name="fieldOrPropertyName">Field or property name</param>
        /// <returns>result = delegate(owner)</returns>
        public static Delegate ConstructFieldOrPropertyGetter(Type tObj, string fieldOrPropertyName)
        {
            ParameterExpression obj = Expression.Parameter(tObj, "Tobj");
            var property = ConstructFieldOrPropertyGetter(obj, fieldOrPropertyName);

            return Expression.Lambda(property, obj).Compile();
        }

        /// <summary>
        /// Construct Function to get field or property value
        /// </summary>
        /// <typeparam name="TObj">property owner type</typeparam>
        /// <typeparam name="TRes">Type of result</typeparam>
        /// <param name="fieldOrPropertyName">Field or property name</param>
        /// <returns>result =Func(owner)</returns>
        public static Func<TObj, TRes> ConstructFieldOrPropertyGetter<TObj, TRes>(string fieldOrPropertyName)
        {
            ParameterExpression param1 = Expression.Parameter(typeof(TObj), "Tobj");
            MemberExpression property = Expression.PropertyOrField(param1, fieldOrPropertyName);

            return Expression.Lambda<Func<TObj, TRes>>(property, param1).Compile();
        }

        /// <summary>
        /// Construct expression for property getter. Used for complex expression constuction.
        /// </summary>
        /// <param name="tObj"></param>
        /// <param name="fieldOrPropertyName">Field or property name</param>
        /// <returns>property as member expression</returns>
        public static MemberExpression ConstructFieldOrPropertyGetter(ParameterExpression tObj, string fieldOrPropertyName)
        {
            var propList = fieldOrPropertyName.SplitExt('.').ToList();
            var property = ConstructFieldOrPropertyGetter(tObj, propList);

            return property as MemberExpression;
        }

        /// <summary>
        /// Construct expression for property getter
        /// </summary>
        /// <param name="property">parent parameter or property</param>
        /// <param name="propList">properties chain</param>
        /// <returns></returns>
        private static Expression ConstructFieldOrPropertyGetter(Expression property, IList<string> propList)
        {
            if (propList.IsEmpty())
                return property;

            var propertyName = propList[0];
            property = Expression.PropertyOrField(property, propertyName);
            propList.RemoveAt(0);
            property = ConstructFieldOrPropertyGetter(property, propList);

            return property;
        }
    }
}
