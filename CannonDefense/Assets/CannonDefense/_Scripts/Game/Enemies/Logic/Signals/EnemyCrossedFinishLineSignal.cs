using GlassyCode.CannonDefense.Game.Enemies.Data;
using Unity.Collections;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic.Signals
{
    public struct EnemyCrossedFinishLineSignal
    {
        public NativeArray<EnemyEffectEntityData> Effects;
        public int Damage;
    }
}