using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Object
{
    public interface IGlassyObjectPool<T> where T : GlassyObjectPoolElement<T>
    {
        ObjectPool<T> Pool { get; }
    }
}