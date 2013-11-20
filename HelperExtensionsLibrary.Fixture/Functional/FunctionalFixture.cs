using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Functional;

namespace HelperExtensionsLibrary.Fixture.Functional
{
    public class FunctionalFixture
    {
        [Fact]
        public void ListComprehensionFixture()
        {

            var query = ListComprehensionHelper.BuildWithOutputFunction<int, int>(input => input["x"] * input["y"])
                .AndWithList(x => new[] { 2, 5, 10 })
                .AndWithList(y => new[] { 8, 10, 11 })
                .AsEnumerable();
            
            var list = query.ToList();

            IList<int> should = new[] {16,20,22,40,50,55,80,100,110};
            list.Should().Count.Exactly(should.Count);
            for (int index = 0 ; index < should.Count; index++)
                list[index].Should().Equal(should[index]);

            query = ListComprehensionHelper.BuildWithOutputFunction<int, int>(input => input["x"] * input["y"])
                .AndWithList(x => new[] { 2, 5, 10 })
                .AndWithList(y => new[] { 8, 10, 11 })
                .AndWithPredicate(input => input["x"] * input["y"] >= 50)
                .AndWithPredicate(input => input["x"] < 10)
                .AsEnumerable();

            list = query.ToList();

            should = new[] { 50, 55 };
            list.Should().Count.Exactly(should.Count);
            for (int index = 0; index < should.Count; index++)
                list[index].Should().Equal(should[index]);

            var listFunc = ListComprehensionHelper.BuildWithOutputFunction<string, int>(input => 1).AsFunction();
            Func<IEnumerable<int>, int> sumFunc = (input) => input.Sum();
            var length = sumFunc(listFunc(new[] { "1", "2", "3", "4", "5" }));

            length.Should().Equal(5);
        }
        
        
        [Fact]
        public void FunctionFixture()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            Func<IEnumerable<int>, int> x = Enumerable.Sum;
            Func<IEnumerable<int>, int> sumFunc = (input) => input.Sum();

            var func1 = new CarriedFunction<int, int, string, string>((param1, param2, param3) => (param1 + param2).ToString()+param3);
            var result = func1.AsFunc()(5, 10, " cool!");
            result.Should().Equal("15 cool!");
            var func2 = func1.PartialyResolve(5);
            result = func2.AsFunc()(10, " cool!");
            result.Should().Equal("15 cool!");
            var func3 = func2.PartialyResolve(10);
            result = func3.AsFunc()(" cool!");
            result.Should().Equal("15 cool!");
            var func4 = func3.PartialyResolve(" cool!");
            result = func4.AsFunc()();
            result.Should().Equal("15 cool!");


            func2.AddMatch(FParamsHelper.Create<int, string>(FParamHelper.Create(11), FParamHelper.Create("x1!")), (param1, param2) => "case1! '" + param2 + "'!");
            func2.AddMatch(FParamsHelper.Create<int, string>(FParamHelper.Create(11),  null), (param, _) => "case2!");
            func2.AddMatch(FParamsHelper.Create<int, string>(null, FParamHelper.Create("x1!")), (_1, _2) => "case3!");
            func2.AddMatch(FParamsHelper.Create<int, string>(FParamHelper.Create(12), FParamHelper.Create("x1!")), (_1, param2) => "case5! '" + param2 + "'!");
            func2.AddMatch(FParamsHelper.Create<int, string>(FParamHelper.Create(12), FParamHelper.Create("x2!")), (_1, param2) => "case6! '" + param2 + "'!");



            result = func2.Invoke(FParamsHelper.Create<int, string>(FParamHelper.Create(11), null));
            result.Should().Equal("case2!");

            func3 = func2.PartialyResolve(11);
            result = func3.Invoke(null);
            result.Should().Equal("case2!");


            result = func2.Invoke(FParamsHelper.Create<int, string>(FParamHelper.Create(11), FParamHelper.Create("x1!")));
            result.Should().Equal("case1! 'x1!'!");
            result = func2.Invoke(FParamsHelper.Create<int, string>(null, FParamHelper.Create("x1!")));
            result.Should().Equal("case3!");
            result = func2.Invoke(FParamsHelper.Create<int, string>(FParamHelper.Create(11), FParamHelper.Create("x2!")));
            result.Should().Equal("case2!");
            result = func2.Invoke(FParamsHelper.Create<int, string>(FParamHelper.Create(12), FParamHelper.Create("x1!")));
            result.Should().Equal("case3!");
            result = func2.Invoke(FParamsHelper.Create<int, string>(FParamHelper.Create(12), FParamHelper.Create("x2!")));
            result.Should().Equal("case6! 'x2!'!");
        }

        [Fact]
        public void FunctionParameterFixture()
        {
            var params1 = FParamsHelper.Create<int, string>(FParamHelper.Create(1), null);
            var params2 = FParamsHelper.Create<int, string>(FParamHelper.Create(1), FParamHelper.Create("xxx"));

            params1.Should().Equal(FParamsHelper.Create<int, string>(FParamHelper.Create(1), null));
            params2.Should().Equal(FParamsHelper.Create<int, string>(FParamHelper.Create(1), FParamHelper.Create("xxx")));

            params1.GetHashCode().Should().Equal(FParamsHelper.Create<int, string>(FParamHelper.Create(1), null).GetHashCode());
            params2.GetHashCode().Should().Equal(FParamsHelper.Create<int, string>(FParamHelper.Create(1), FParamHelper.Create("xxx")).GetHashCode());
        }


    }


}
