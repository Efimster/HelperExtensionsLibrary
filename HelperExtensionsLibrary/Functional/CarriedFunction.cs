//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

//namespace HelperExtensionsLibrary.Functional
//{
//    public interface ICarriedFunction
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="parameters"></param>
//        /// <returns></returns>
//        ICarriedFunction Invoke(FParams parameters);
//        /// <summary>
//        /// Partially resolves function
//        /// </summary>
//        /// <param name="parameters">value of any parameter</param>
//        /// <returns>More specific function</returns>
//        ICarriedFunction PartialyResolve(FParams parameters);
//    }


//    public class CarriedFunction<T1> : ICarriedFunction
//    {
//        public Expression<Func<T1>> ExpFunc { get; set; }

//        public CarriedFunction(Expression<Func<T1>> func)
//        {
//            ExpFunc = func;
//        }

//        public Func<T1> AsFunc()
//        {
//            return ExpFunc.Compile();
//        }

//        public ICarriedFunction Invoke(FParams parameters)
//        {
//            throw new NotImplementedException();
//        }

//        public ICarriedFunction PartialyResolve(FParams parameters)
//        {
//            throw new NotImplementedException();
//        }
//    }


//    public class CarriedFunction<T1, T2> : ICarriedFunction
//    {
//        public Expression<Func<T1, T2>> ExpFunc { get; set; }
//        private IDictionary<FParams<T1>, Expression<Func<T1, T2>>> Matches { get; set; }


//        public CarriedFunction(Expression<Func<T1, T2>> func)
//        {
//            ExpFunc = func;
//            Matches = new Dictionary<FParams<T1>, Expression<Func<T1, T2>>>();
//        }

//        public void AddMatch(FParams<T1> match, Expression<Func<T1, T2>> func)
//        {
//            Matches.Add(match, func);

//        }

//        public Func<T1, T2> AsFunc()
//        {
//            return ExpFunc.Compile();
//        }



//        public ICarriedFunction Invoke(FParams parameters)
//        {
//            throw new NotImplementedException();
//        }

//        public ICarriedFunction PartialyResolve(FParams parameters)
//        {
//            var paramValues = parameters as FParams<T1>;
//            if (paramValues == null)
//                throw new Exception("wrong parameters type");

//            Expression<Func<T1, T2>> expMatch;
//            if (!Matches.TryGetValue(paramValues, out expMatch))
//            {
//                expMatch = ExpFunc;
//            }


//            var exp = Expression.Lambda<Func<T2>>(expMatch.Body);
//            MyVisitor visitor = new MyVisitor(expMatch.Parameters[0], Expression.Constant(paramValues.Param1));
//            Expression<Func<T2>> newExp = (Expression<Func<T2>>)visitor.Visit(exp);


//            return new CarriedFunction<T2>(newExp);
//        }
//    }

//    public class CarriedFunction<T1, T2, T3> : ICarriedFunction
//    {
//        public Expression<Func<T1, T2, T3>> ExpFunc { get; set; }
//        private Lazy<Func<T1, T2, T3>> CompFunc { get; set; }
//        private IDictionary<FParams<T1, T2>, Expression<Func<T1, T2, T3>>> Matches { get; set; }



//        public CarriedFunction(Expression<Func<T1, T2, T3>> func)
//        {
//            ExpFunc = func;
//            CompFunc = new Lazy<Func<T1, T2, T3>>(() => ExpFunc.Compile());
//            Matches = new Dictionary<FParams<T1, T2>, Expression<Func<T1, T2, T3>>>();
//        }

//        public void AddMatch(FParams<T1, T2> match, Expression<Func<T1, T2, T3>> func)
//        {
//            Matches.Add(match, func);
//        }

//        public CarriedFunction<T2, T3> PartialyResolve(T1 parameter)
//        {
//            Expression<Func<T1, T2, T3>> expMatch;
//            if (!Matches.TryGetValue(FParamsHelper.Create<T1, T2>(FParamHelper.Create(parameter), null), out expMatch))
//            {
//                expMatch = ExpFunc;
//            }


//            var exp = Expression.Lambda<Func<T2, T3>>(expMatch.Body, expMatch.Parameters[1]);
//            MyVisitor visitor = new MyVisitor(expMatch.Parameters[0], Expression.Constant(parameter));
//            Expression<Func<T2, T3>> newExp = (Expression<Func<T2, T3>>)visitor.Visit(exp);

//            return new CarriedFunction<T2, T3>(newExp);
//        }

//        public T3 Invoke(FParams<T1, T2> parameters)
//        {
//            Expression<Func<T1, T2, T3>> exp;
//            if (!Matches.TryGetValue(parameters, out exp))
//            {
//                exp = ExpFunc;
//            }

//            return exp.Compile()(parameters.Param1, parameters.Param2);
//        }

//        public ICarriedFunction Invoke(FParams parameters)
//        {
//            throw new NotImplementedException();
//        }
//        /// <summary>
//        /// Partially resolves function
//        /// </summary>
//        /// <param name="parameters">value of any parameter</param>
//        /// <returns>More specific function</returns>
//        public ICarriedFunction PartialyResolve(FParams parameters)
//        {
//            var paramValues = parameters as FParams<T1, T2>;
//            if (paramValues == null)
//                throw new Exception("wrong parameters type");
            
//            Expression<Func<T1, T2, T3>> expMatch;


//            if (!Matches.TryGetValue(paramValues, out expMatch))
//            {
//                expMatch = ExpFunc;
//            }

//            var expL = Expression.Lambda(expMatch.Body, expMatch.Parameters[1]);

//            var exp = Expression.Lambda<Func<T2, T3>>(expMatch.Body, expMatch.Parameters[1]);
//            MyVisitor visitor = new MyVisitor(expMatch.Parameters[0], Expression.Constant(paramValues.Param1));
//            Expression<Func<T2, T3>> newExp = (Expression<Func<T2, T3>>)visitor.Visit(exp);

//            return new CarriedFunction<T2, T3>(newExp);
//        }
//    }




//    public class MyVisitor : ExpressionVisitor
//    {
//        private ParameterExpression ParamToChange { get; set; }
//        private ConstantExpression Constant { get; set; }

//        public MyVisitor(ParameterExpression paramToChange, ConstantExpression constant)
//        {
//            ParamToChange = paramToChange;
//            Constant = constant;
//        }


//        protected override Expression VisitParameter(ParameterExpression node)
//        {
//            if (node != ParamToChange)
//                return base.VisitParameter(node);


//            return Constant;

//        }
//    }


//}
