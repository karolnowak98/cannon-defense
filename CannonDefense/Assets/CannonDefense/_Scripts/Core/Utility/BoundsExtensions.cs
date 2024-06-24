using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class BoundsExtensions
    {
        public static Rect GetXZRect(this Bounds bounds)
        {
            var size = bounds.size;
            var min = bounds.min;

            return new Rect(min.x, min.z, size.x, size.z);
        }
        
        public static Rect GetXYRect(this Bounds bounds)
        {
            var size = bounds.size;
            var min = bounds.min;

            return new Rect(min.x, min.y, size.x, size.y);
        }
    }
}