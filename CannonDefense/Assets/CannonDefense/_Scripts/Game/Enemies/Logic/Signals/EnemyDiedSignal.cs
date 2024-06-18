using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic.Signals
{
    public struct EnemyDiedSignal
    {
        public IEnemy Enemy;
        public EnemyEffectEntity[] Effects;
        public int Score;
        public int Experience;
    }
}