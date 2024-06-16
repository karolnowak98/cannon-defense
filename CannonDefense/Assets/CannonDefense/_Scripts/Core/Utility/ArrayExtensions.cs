using System;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class ArrayExtensions
    {
        public static T GetRandomElement<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Array is null or empty.");
            }
        
            var index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }
    }
}