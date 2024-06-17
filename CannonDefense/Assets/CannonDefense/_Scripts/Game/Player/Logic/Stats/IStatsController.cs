using System;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public interface IStatsController
    {
        event Action<int> OnScoreUpdated;
        event Action<int> OnLevelUp;
        event Action OnPlayerDied;
        void Reset();
        void EnemyAttackedHandler(EnemyAttackedSignal enemyAttackedSignal);
        void EnemyKilledHandler(EnemyKilledSignal enemyKilledSignal);
    }
}