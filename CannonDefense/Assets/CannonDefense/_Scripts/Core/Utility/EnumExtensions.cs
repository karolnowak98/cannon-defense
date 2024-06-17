using System;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class EnumExtensions
    {
        public static T GetRandomValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var random = new Random();
            return (T)values.GetValue(random.Next(values.Length));
        }
    }
}