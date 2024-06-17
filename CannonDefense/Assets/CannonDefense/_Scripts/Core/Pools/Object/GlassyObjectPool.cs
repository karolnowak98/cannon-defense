using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Object
{
    public abstract class GlassyObjectPool<T> : IGlassyObjectPool<T> where T : GlassyObjectPoolElement<T>
    {
        protected readonly T Prefab;

        public IObjectPool<T> Pool { get; }
        
        protected GlassyObjectPool(T prefab, int initialSize = 10, int maxSize = 10000)
        {
            Prefab = prefab;

            Pool = new ObjectPool<T>(CreateElement, OnGetElementFromPool, OnReleaseElementToPool, OnDestroyElement, false, initialSize, maxSize);
        }
        
        public void Clear()
        {
            Pool.Clear();
        }
        
        protected virtual T CreateElement()
        {
            var element = UnityEngine.Object.Instantiate(Prefab);
            element.Pool = Pool;
            element.Reset();
            return element;
        }

        protected virtual void OnGetElementFromPool(T element)
        {
            element.Reset();
        }

        protected virtual void OnReleaseElementToPool(T element)
        {
            element.Disable();
        }

        protected virtual void OnDestroyElement(T element)
        {
            element.Destroy();
        }
    }
}