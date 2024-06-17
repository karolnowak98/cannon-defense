using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Object
{
    public class GlassyObjectRandomPool<T> : IGlassyObjectPool<T> where T : GlassyObjectPoolElement<T>
    {
        protected readonly T[] Prefabs;

        public IObjectPool<T> Pool { get; }

        protected GlassyObjectRandomPool(T[] prefabs, int initialSize = 10, int maxSize = 10000)
        {
            Prefabs = prefabs;

            Pool = new ObjectPool<T>(CreateElement, OnGetElementFromPool, OnReleaseElementToPool, OnDestroyElement, true, initialSize, maxSize);
        }
        
        public virtual void Clear()
        {
            Pool.Clear();
        }
        
        protected virtual T CreateElement()
        {
            var randomPrefab = Prefabs.GetRandomElement();
            var element = UnityEngine.Object.Instantiate(randomPrefab);
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