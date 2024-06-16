using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Core.Pools.Dictionary
{
    public abstract class GlassyDictionaryPoolElement<T, T2> : GlassyMonoBehaviour where T2 : GlassyDictionaryPoolElement<T, T2>
    {
        public DictionaryPool<T, T2> Pool { get; set; }

        public abstract void Reset();
    }
}