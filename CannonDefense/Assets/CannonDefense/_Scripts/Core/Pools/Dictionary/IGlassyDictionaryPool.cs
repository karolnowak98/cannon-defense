using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Dictionary
{
    public interface IGlassyDictionaryPool<T, T2> where T2 : GlassyDictionaryPoolElement<T, T2>
    {
        DictionaryPool<T, T2> Pool { get; }
    }
}