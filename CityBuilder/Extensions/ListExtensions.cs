using System;
using System.Collections.Generic;

namespace CityBuilder.Extensions
{
    public static class ListExtensions
    {
        private static Random _random = new Random();
        public static T Random<T>(this IList<T> list)
        {
            var max = list.Count - 1;
            var index = _random.Next(max);
            return list[index];
        }
    }
}