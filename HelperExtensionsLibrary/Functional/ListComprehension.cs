using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Functional
{
    /// <summary>
    /// Functional list comprehension
    /// </summary>
    /// <typeparam name="T">type of list comprehension items</typeparam>
    public class ListComprehension<T,TRes> : IEnumerable<TRes>, IListComprehensionBuilder<T,TRes>
    {
        /// <summary>
        /// Input sets
        /// </summary>
        IDictionary<string, Func<string, IEnumerable<T>>> InputSets { get; set; }
        /// <summary>
        /// output function
        /// </summary>
        Func<IDictionary<string,T>,TRes> OutputFunction { get; set; }

        /// <summary>
        /// predicates
        /// </summary>
        IList<Predicate<IDictionary<string,T>>> Predicates { get; set; }

        internal ListComprehension()
        {
            InputSets = new Dictionary<string, Func<string, IEnumerable<T>>>(2);
            Predicates = new List<Predicate<IDictionary<string, T>>>(1);
        }


        /// <summary>
        /// Add input set
        /// </summary>
        /// <param name="input">input set</param>
        /// <returns>ListComprehension</returns>
        private ListComprehension<T,TRes> AddInput(Expression<Func<string, IEnumerable<T>>>  input)
        {
            string paramName = input.Parameters[0].Name;

            InputSets.Add(paramName, input.Compile());
            return this;
        }
        /// <summary>
        /// Set output function
        /// </summary>
        /// <param name="output">output function</param>
        /// <returns>list comprehension</returns>
        private ListComprehension<T,TRes> SetOutput(Func<IDictionary<string, T>, TRes> output)
        {
            OutputFunction = output;
            return this;
        }

      

        /// <summary>
        /// Add predicate
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>list comprehension</returns>
        private ListComprehension<T,TRes> AddPredicate(Predicate<IDictionary<string,T>> predicate)
        {
            Predicates.Add(predicate);
            return this;
        }


        private IEnumerable<IDictionary<string, T>> GoThroughInput(IEnumerator<KeyValuePair<string, IEnumerator<T>>> enumeratorsEnumerator,
            KeyValuePair<string, IEnumerator<T>> enumerator,
            IDictionary<string, T> outputFunctionInput)
        {
            enumerator.Value.Reset();
            var next = enumeratorsEnumerator.MoveNext() ? enumeratorsEnumerator.Current : (KeyValuePair<string, IEnumerator<T>>?)null;

            try
            {
                while (enumerator.Value.MoveNext())
                {
                    outputFunctionInput[enumerator.Key] = enumerator.Value.Current;
                    if (next.HasValue)
                        foreach (var item in GoThroughInput(enumeratorsEnumerator, next.Value, outputFunctionInput))
                            yield return item;
                    else
                        yield return outputFunctionInput;

                }
            }
            finally
            {
                enumerator.Value.Dispose();
            }


        }

        public IEnumerator<TRes> GetEnumerator()
        {
            IDictionary<string, IEnumerator<T>> enumerators = new Dictionary<string, IEnumerator<T>>(InputSets.Count);

            foreach (var inputFunc in InputSets)
            {
                var input = inputFunc.Value.Invoke(string.Empty);
                if (input != null)
                {
                    enumerators.Add(inputFunc.Key, input.GetEnumerator());
                }
            }

            IDictionary<string, T> outputFunctionInput = new Dictionary<string, T>(enumerators.Count);

            try
            {
                outputFunctionInput.Clear();
                var enumeratorsEnumerator = enumerators.GetEnumerator();

                try
                {
                    if (enumeratorsEnumerator.MoveNext())
                    {
                        var enumerator = enumeratorsEnumerator.Current;
                        foreach (var item in GoThroughInput(enumeratorsEnumerator, enumerator, outputFunctionInput))
                        {
                            if (Predicates == null || Predicates.Count == 0)
                            {
                                yield return OutputFunction(item);
                                continue;
                            }

                            bool suits = true;
                            foreach (var predicate in Predicates)
                                if (!(suits = predicate(item)))
                                    break;

                            if (suits)
                                yield return OutputFunction(item);
                        }
                    }

                }
                finally
                {
                    enumeratorsEnumerator.Dispose();
                }
            }
            finally
            {
                foreach (var enumerator in enumerators)
                    enumerator.Value.Dispose();

                enumerators.Clear();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        IListComprehensionBuilder<T, TRes> IListComprehensionBuilder<T, TRes>.AndWithPredicate(Predicate<IDictionary<string, T>> predicate)
        {
            return this.AddPredicate(predicate);
        }

        IEnumerable<TRes> IListComprehensionBuilder<T, TRes>.AsEnumerable()
        {
            return this;
        }

        IListComprehensionBuilder<T, TRes> IListComprehensionBuilder<T, TRes>.AndWithList(Expression<Func<string, IEnumerable<T>>> input)
        {
            return this.AddInput(input);
        }

        Func<IEnumerable<T>, IEnumerable<TRes>> IListComprehensionBuilder<T, TRes>.AsFunction()
        {
            Func<IEnumerable<T>, IEnumerable<TRes>> func = (list =>
            {
                AddInput(x=> list);
                return this;
            });

            return func;
        }


        IListComprehensionBuilder<T,TRes> IListComprehensionBuilder<T,TRes>.WithOutputFunction(Func<IDictionary<string, T>, TRes> output)
        {
            return this.SetOutput(output);
        }
    }
}
