using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public interface IStatsController
    {
        int CurrentDamage { get; }
        void Reset();
        void EnemyAttackedHandler(EnemyCrossedFinishLine enemyCrossedFinishLine);
        void EnemyKilledHandler(EnemyDiedSignal enemyDiedSignal);
    }
}