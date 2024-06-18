using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic.Signals
{
    public struct EnemyDiedSignal
    {
        public EnemyEffectEntity[] Effects;
        public int Score;
        public int Experience;
    }
}