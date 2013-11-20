using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HelperExtensionsLibrary.Functional
{
    public interface ICarriedFunction
    {
        ICarriedFunction Invoke(FParams parameters);
        ICarriedFunction PartialyResolve(FParams parameters);
    }


    public class CarriedFunction<T1>
    {
        public Expression<Func<T1>> ExpFunc { get; set; }

        public CarriedFunction(Expression<Func<T1>> func)
        {
            ExpFunc = func;
        }

        public Func<T1> AsFunc()
        {
            return ExpFunc.Compile();
        }
    }


    public class CarriedFunction<T1, T2>
    {
        public Expression<Func<T1, T2>> ExpFunc { get; set; }
        private IDictionary<Tuple<T1>, Expression<Func<T1, T2>>> Matches { get; set; }

        public CarriedFunction(Expression<Func<T1, T2>> func)
        {
            ExpFunc = func;
            Matches = new Dictionary<Tuple<T1>, Expression<Func<T1, T2>>>();
        }

        public void AddMatch(Tuple<T1> match, Expression<Func<T1, T2>> func)
        {
            Matches.Add(match, func);

        }

        public CarriedFunction<T2> PartialyResolve(T1 parameter)
        {
            Expression<Func<T1, T2>> expMatch;
            if (!Matches.TryGetValue(Tuple.Create(parameter), out expMatch))
            {
                expMatch = ExpFunc;
            }


            var exp = Expression.Lambda<Func<T2>>(expMatch.Body);
            MyVisitor visitor = new MyVisitor(expMatch.Parameters[0], Expression.Constant(parameter));
            Expression<Func<T2>> newExp = (Expression<Func<T2>>)visitor.Visit(exp);


            return new CarriedFunction<T2>(newExp);
        }

        public Func<T1, T2> AsFunc()
        {
            return ExpFunc.Compile();
        }

        public T2 Invoke(T1 param1)
        {
            Expression<Func<T1, T2>> exp;
            if (!Matches.TryGetValue(Tuple.Create(param1), out exp))
            {
                exp = ExpFunc;
            }

            return exp.Compile()(param1);
        }
    }

    public class CarriedFunction<T1, T2, T3> 
    {
        public Expression<Func<T1, T2, T3>> ExpFunc { get; set; }
        private Lazy<Func<T1, T2, T3>> CompFunc { get; set; }
        private IDictionary<FParams<T1, T2>, Expression<Func<T1, T2, T3>>> Matches { get; set; }



        public CarriedFunction(Expression<Func<T1, T2, T3>> func)
        {
            ExpFunc = func;
            CompFunc = new Lazy<Func<T1, T2, T3>>(() => ExpFunc.Compile());
            Matches = new Dictionary<FParams<T1, T2>, Expression<Func<T1, T2, T3>>>();
        }

        public void AddMatch(FParams<T1, T2> match, Expression<Func<T1, T2, T3>> func)
        {
            Matches.Add(match, func);
        }

        public CarriedFunction<T2, T3> PartialyResolve(T1 parameter)
        {
            Expression<Func<T1, T2, T3>> expMatch;
            if (!Matches.TryGetValue(FParamsHelper.Create<T1, T2>(FParamHelper.Create(parameter), null), out expMatch))
            {
                expMatch = ExpFunc;
            }


            var exp = Expression.Lambda<Func<T2, T3>>(expMatch.Body, expMatch.Parameters[1]);
            MyVisitor visitor = new MyVisitor(expMatch.Parameters[0], Expression.Constant(parameter));
            Expression<Func<T2, T3>> newExp = (Expression<Func<T2, T3>>)visitor.Visit(exp);

            return new CarriedFunction<T2, T3>(newExp);
        }


        public Func<T1, T2, T3> AsFunc()
        {
            return CompFunc.Value;
        }

        public T3 Invoke(FParams<T1, T2> parameters)
        {
            Expression<Func<T1, T2, T3>> exp;
            if (!Matches.TryGetValue(parameters, out exp))
            {
                exp = ExpFunc;
            }

            return exp.Compile()(parameters.Param1, parameters.Param2);
        }

    }

    public class CarriedFunction<T1, T2, T3, T4>
    {
        private Expression<Func<T1, T2, T3, T4>> ExpFunc { get; set; }
        private Lazy<Func<T1, T2, T3, T4>> CompFunc { get; set; }

        public CarriedFunction(Expression<Func<T1, T2, T3, T4>> func)
        {
            ExpFunc = func;
            CompFunc = new Lazy<Func<T1, T2, T3, T4>>(() => ExpFunc.Compile());
        }

        public CarriedFunction<T2, T3, T4> PartialyResolve(T1 parameter)
        {
            var exp = Expression.Lambda<Func<T2, T3, T4>>(ExpFunc.Body, ExpFunc.Parameters[1], ExpFunc.Parameters[2]);
            MyVisitor visitor = new MyVisitor(ExpFunc.Parameters[0], Expression.Constant(parameter));
            var newExp = (Expression<Func<T2, T3, T4>>)visitor.Visit(exp);

            return new CarriedFunction<T2, T3, T4>(newExp);
        }

        public Func<T1, T2, T3, T4> AsFunc()
        {
            return CompFunc.Value;
        }
    }


    public class MyVisitor : ExpressionVisitor
    {
        private ParameterExpression ParamToChange { get; set; }
        private ConstantExpression Constant { get; set; }

        public MyVisitor(ParameterExpression paramToChange, ConstantExpression constant)
        {
            ParamToChange = paramToChange;
            Constant = constant;
        }


        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node != ParamToChange)
                return base.VisitParameter(node);


            return Constant;

        }
    }


}
