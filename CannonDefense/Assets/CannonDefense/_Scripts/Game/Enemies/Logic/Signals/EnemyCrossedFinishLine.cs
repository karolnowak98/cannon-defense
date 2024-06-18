using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic.Signals
{
    public struct EnemyCrossedFinishLine
    {
        public IEnemy Enemy;
        public EnemyEffectEntity[] Effects;
        public int Damage;
    }
}