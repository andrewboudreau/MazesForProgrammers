﻿using System.Collections.Generic;
using System.Linq;

namespace MazesForProgrammers.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static int[] IntegersFromCsv(this string csv)
        {
            return csv.Split(',').Select(x => int.Parse(x.Trim())).ToArray();
        }

        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Any() == false;
        }

        public static T Sample<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(RandomSource.Random.Next(collection.Count()));
        }

        public static IEnumerable<T> RemoveNulls<T>(this IEnumerable<T> items)
        {
            return items.Where(x => x != null);
        }
    }
}