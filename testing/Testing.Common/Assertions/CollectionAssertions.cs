using FluentAssertions;

namespace Testing.Common.Assertions
{
    public static class CollectionAssertions
    {
        /// <summary>
        ///     Asserts that two collections are equivalent using a comparison
        ///     function
        /// </summary>
        public static void ShouldBeEquivalentTo<T1, T2>(
            this IEnumerable<T1> col1,
            IEnumerable<T2> col2, Func<T1, T2, bool> comparisonFunc)

        {
            col1.Count().Should().Be(col2.Count(),
                "Both collections must have the same number of items");

            var caseA = col1.All(x => col2.Any(y => comparisonFunc(x, y)));

            var caseB = col2.All(x => col1.Any(y => comparisonFunc(y, x)));

            caseA.Should()
                .BeTrue(
                    "All items in collection 1 must have an equivalent in collection 2");

            caseB.Should()
                .BeTrue(
                    "All items in collection 2 must have an equivalent in collection 1");
        }

        public static void ShouldBeEquivalentTo<T>(
            this IEnumerable<T> col1,
            IEnumerable<T> col2)
        where T:class
        {
            col1.ShouldBeEquivalentTo(col2,(x,y)=>x.Equals(y));
        }
    }
}