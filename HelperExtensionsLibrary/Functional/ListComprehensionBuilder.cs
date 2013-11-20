using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Functional
{
    /// <summary>
    /// Returns list comprehension as IEnumerable
    /// </summary>
    /// <typeparam name="T">list comprehension items type</typeparam>
    public interface IListComprehensionBuilder<T,TRes>
    {
        /// <summary>
        /// Returns list comprehension as IEnumerable
        /// </summary>
        /// <param name="output">output function</param>
        /// <returns>list comprehension</returns>
        IEnumerable<TRes> AsEnumerable();
        /// <summary>
        /// Returns list comprehension as Func
        /// </summary>
        /// <param name="output">output function</param>
        /// <returns>list comprehension</returns>
        Func<IEnumerable<T>, IEnumerable<TRes>> AsFunction();
        /// <summary>
        /// Add input set
        /// </summary>
        /// <param name="input">input set</param>
        /// <returns>list comprehension. Follow with .AndWithList or .AndWithPredicate</returns>
        IListComprehensionBuilder<T,TRes> AndWithList(Expression<Func<string, IEnumerable<T>>> input);
        /// <summary>
        /// Add predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>list comprehension. Follow with .AndWithPredicate or .Resolve</returns>
        IListComprehensionBuilder<T,TRes> AndWithPredicate(Predicate<IDictionary<string, T>> predicate);

        /// <summary>
        ///  Add output Function
        /// </summary>
        /// <param name="output">output function</param>
        /// <returns>list comprehension</returns>
        IListComprehensionBuilder<T,TRes> WithOutputFunction(Func<IDictionary<string, T>, TRes> output);

    }

    
    
    public static class ListComprehensionHelper
    {
        ///// <summary>
        ///// Starts build list comprehension
        ///// </summary>
        ///// <typeparam name="T">type of list comprehension items</typeparam>
        ///// <param name="input">input set</param>
        ///// <returns>list comprehension. Follow with .AndWithList or .AndWithPredicate</returns>
        //public static IListComprehensionBuilder<T,TRes> BuildWithList<T,TRes>(Expression<Func<string, IEnumerable<T>>> input)
        //{
        //    IListComprehensionBuilder<T,> builder = new ListComprehension<T>();
        //    return builder.AndWithList(input);
        //}

        public static IListComprehensionBuilder<T,TRes> BuildWithOutputFunction<T,TRes>(Func<IDictionary<string, T>, TRes> output)
        {
            IListComprehensionBuilder<T,TRes> builder = new ListComprehension<T,TRes>();
            return builder.WithOutputFunction(output);
        }
        
      
    }
}
