using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Dictionary
{
    public class GlassyDictionaryRandomPoolType<T, T2> : IGlassyDictionaryPool<T, T2> where T2 : GlassyDictionaryPoolElement<T, T2>
    {
        protected readonly T[] Prefabs;

        public DictionaryPool<T, T2> Pool { get; private set; }

        protected GlassyDictionaryRandomPoolType(T[] prefabs)
        {
            Prefabs = prefabs;

            Pool = new DictionaryPool<T, T2>();
        }
    }
}