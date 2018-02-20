using System;

namespace CityBuilder.Util
{
    public static class RandomEnum
    {
        private static readonly Random Random;

        static RandomEnum()
        {
            Random = new Random();
        }

        public static T Get<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var index = Random.Next(values.Length);
            T result = (T) values.GetValue(index);
            return result;
        }
    }
}