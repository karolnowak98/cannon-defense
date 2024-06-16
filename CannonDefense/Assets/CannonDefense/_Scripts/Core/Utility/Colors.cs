using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class Colors
    {
        public static Color GetRandomColor()
        {
            return new Color(Random.value, Random.value, Random.value);
        }
    }
}