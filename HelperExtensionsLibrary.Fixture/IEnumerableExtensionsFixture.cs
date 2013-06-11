using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperExtensionsLibrary.IEnumerable;
using Should.Fluent;
using Xunit;

namespace HelperExtensionsLibrary.Fixture
{
    public class IEnumerableExtensionsFixture
    {
        [Fact]
        public void ForEachFixture()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            int idx = 0;

            list.ForEach(item => item.Should().Equal(list[idx++]));
            list.ForEach((item, index) => {
                item.Should().Be.LessThan(4);
                return index < 2; 
            });
        }

        [Fact]
        public void ConvertFixture()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            int idx = 0;

            list.Convert(item => item*2).ForEach(item => item.Should().Equal(list[idx++]*2));
        }


        [Fact]
        public void JoinByIndexFixture()
        {
            var left = new[] { 1, 2, 3, 4, 5 };
            var right = new[] { 10, 20, 30, 40, 50 };
            int idx = 0;

            left.JoinByIndex(right, (litem,ritem) => litem + ritem).ForEach(item => item.Should().Equal(left[idx]+right[idx++]));
        }


        [Fact]
        public void EquelsByIndexFixture()
        {
            var first = new[] { 1, 2, 3, 4, 5 };
            var second = new[] { 1, 2, 3, 4, 5 };

            first.EquelsByIndex(new[] { 1, 2, 3, 4, 5 }).Should().Be.True();
            first.EquelsByIndex(new[] { 1, 2, 3, 4 }).Should().Be.False();
            first.EquelsByIndex(new[] { 1, 2, 3, 4, 5, 6 }).Should().Be.False();
            first.EquelsByIndex(new[] { 2, 1, 3, 4, 5 }).Should().Be.False(); 
        }

        [Fact]
        public void ElementAtOrDefaultFixture()
        {
            var list = new[] { 1, 2, 3, 4, 5 };
            list.ElementAtOrDefault(2,-10).Should().Equal(3);
            list.ElementAtOrDefault(5, -10).Should().Equal(-10);
            list.ElementAtOrDefault(-2, -10).Should().Equal(-10);

        }

        [Fact]
        public void FirstIndexEquelsFixture()
        {
            var list = new[] { 5, 4, 3, 2, 1 };
            list.FirstIndexEquels(2).Should().Equal(3);
            list.FirstIndexEquels(6).Should().Equal(-1);
        }


    }
}
