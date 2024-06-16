using UnityEngine;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pool
{
    public class GlassyObjectPool<T> where T : GlassyObjectPoolElement<T>
    {
        protected readonly ObjectPool<T> Pool;
        protected readonly T Prefab;

        protected GlassyObjectPool(T prefab, int initialSize = 10, int maxSize = 10000)
        {
            Prefab = prefab;

            Pool = new ObjectPool<T>(CreateElement, OnGetElementFromPool, OnReleaseElementToPool, OnDestroyElement, true, initialSize, maxSize);
        }
        
        protected virtual T CreateElement()
        {
            var element = Object.Instantiate(Prefab);
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