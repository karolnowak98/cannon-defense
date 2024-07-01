using UnityEngine;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Object
{
    public abstract class GlassyObjectPool<T> : IGlassyObjectPool<T> where T : GlassyObjectPoolElement<T>
    {
        protected readonly T Prefab;
        protected Transform Parent;

        public IObjectPool<T> Pool { get; private set; }
        
        protected GlassyObjectPool(T prefab, Transform parent, int initialSize = 10, int maxSize = 10000)
        {
            Prefab = prefab;
            Parent = parent;
            
            Pool = new ObjectPool<T>(CreateElement, OnGetElementFromPool, OnReleaseElementToPool, OnDestroyElement, true, initialSize, maxSize);
        }
        
        public void Clear()
        {
            Pool.Clear();
        }
        
        public void SetPoolParent(Transform parent)
        {
            Parent = parent;
        }
        
        protected virtual T CreateElement()
        {
            var element = UnityEngine.Object.Instantiate(Prefab);
            element.Pool = Pool;
            return element;
        }

        protected virtual void OnGetElementFromPool(T enemy)
        {
            enemy.Reset();
        }

        protected virtual void OnReleaseElementToPool(T element)
        {
            if (element.IsActive)
            {
                element.Disable();
            }
        }

        protected virtual void OnDestroyElement(T element)
        {
            element.Destroy();
        }
    }
}