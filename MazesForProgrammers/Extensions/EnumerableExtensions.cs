using System;
using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.Extensions
{
    public static partial class EnumerableExtensions
    {
        private readonly static Random Random = new Random();

        public static int[] IntegersFromCsv(this string csv)
        {
            return csv.Split(",").Select(x => int.Parse(x.Trim())).ToArray();
        }

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }

        public static IEnumerable<(T, T)> MakePairs<T>(this IEnumerable<T> set)
        {
            var i = 0;
            foreach (var item in set)
            {
                foreach (var other in set.Skip(i++))
                {
                    if (!other.Equals(item))
                    {
                        yield return (item, other);
                    }
                }
            }
        }

        public static IEnumerable<(T Yours, T Other)> MakePairs<T>(this IEnumerable<T> set, T item)
        {
            foreach (var other in set)
            {
                if (!other.Equals(item))
                {
                    yield return (item, other);
                }
            }
        }

        public static T Sample<T>(this ICollection<T> collection)
        {
            if (collection is null || collection.IsEmpty())
            {
                return default;
            }

            return collection.ElementAt(Random.Next(collection.Count));
        }
    }
}