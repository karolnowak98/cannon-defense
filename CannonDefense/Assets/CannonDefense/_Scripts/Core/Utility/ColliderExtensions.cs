using UnityEngine;

namespace GlassyCode.CannonDefense.Core.Utility
{
    public static class ColliderExtensions
    {
        public static Vector3 GetRandomPointInCollider(this Collider collider)
        {
            var center = collider.bounds.center;
            var size = collider.bounds.size;

            var randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
            var randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
            var randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

            return new Vector3(randomX, randomY, randomZ);
        }
    }
}