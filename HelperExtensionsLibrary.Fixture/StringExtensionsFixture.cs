using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Should.Fluent;
using HelperExtensionsLibrary.Strings;

namespace HelperExtensionsLibrary.Fixture
{
    public class StringExtensionsFixture
    {
        /// <summary>
        /// Splits string to iterative collection by delimiter
        /// </summary>
        [Fact]
        public void SplitExtFixture()
        {
            var list = "|1q|2w|3e||".SplitExt('|').ToList();
            list.Should().Count.Exactly(3);
            list[0].Should().Equal("1q");
            list[1].Should().Equal("2w");
            list[2].Should().Equal("3e");


            list = new[] { "1q|2w|3e", "4r", "5t|6y|" }.SplitExt('|').ToList();
            list.Should().Count.Exactly(6);
            list[0].Should().Equal("1q");
            list[1].Should().Equal("2w");
            list[2].Should().Equal("3e");
            list[3].Should().Equal("4r");
            list[4].Should().Equal("5t");
            list[5].Should().Equal("6y");
        }


        /// <summary>
        /// Join collection elemens to string using delimiter
        /// </summary>
        [Fact]
        public void Join2StringFixture()
        {
            var strings = new[] { "1q", "2w", "3e" };
            strings.Join2String(',').Should().Equal("1q,2w,3e");
        }

        /// <summary>
        /// string.IsNullOrEmpty(value) counterpart
        /// </summary>
        [Fact]
        public void IsEmptyFixture()
        {
            string.Empty.IsEmpty().Should().Be.True();
            ((string)null).IsEmpty().Should().Be.True();
            "x".IsEmpty().Should().Be.False();
        }
    }
}
