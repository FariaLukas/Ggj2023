using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public static class HelperRandom
    {
        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length == 0)
                return default(T);

            return array[Random.Range(0, array.Length)];
        }
        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T GetRandomButNotSame<T>(this List<T> list, T unique)
        {
            var copy = new List<T>(list);

            if (unique != null)
                copy.Remove(unique);

            return copy[Random.Range(0, copy.Count)];
        }
    }
}