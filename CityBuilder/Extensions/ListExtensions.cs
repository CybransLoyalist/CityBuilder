using System;
using System.Collections.Generic;

namespace CityBuilder.Extensions
{
    public static class ListExtensions
    {
        private static Random _random;

        public static void SetRandomizer(Random random)
        {
            _random = random;
        }

        static ListExtensions()
        {
            _random = new Random();
        }

        public static T Random<T>(this IList<T> list)
        {
            var max = list.Count;
            var index = _random.Next(max);
         
            return list[index];
        }
    }
}