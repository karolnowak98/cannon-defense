using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class BoundsExtensions
    {
        public static Rect GetRect(this Bounds bounds)
        {
            var size = bounds.size;
            var min = bounds.min;

            return new Rect(min.x, min.z, size.x, size.z);
        }
    }
}