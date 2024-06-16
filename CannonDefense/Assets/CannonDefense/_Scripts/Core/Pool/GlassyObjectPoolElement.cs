using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pool
{
    public abstract class GlassyObjectPoolElement<T> : GlassyMonoBehaviour where T : GlassyObjectPoolElement<T>
    {
        public ObjectPool<T> Pool { get; set; }

        public abstract void Reset();
    }
}