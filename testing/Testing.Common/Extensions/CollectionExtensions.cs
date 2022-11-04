namespace Testing.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToCollection<T>(this T item,
            params T[] otherItems)
        {
            var result = new List<T>()
            {
                item
            };

            result.AddRange(otherItems);

            return result;
        }
    }
}