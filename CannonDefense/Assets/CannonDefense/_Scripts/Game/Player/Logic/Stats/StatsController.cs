using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Data.Stats;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public sealed class StatsController : IStatsController
    {
        private readonly SignalBus _signalBus;
        private readonly StatsData _statsData;
        
        private Stats _currentStats;

        public int CurrentDamage => _currentStats.Damage;
        
        public StatsController(SignalBus signalBus, StatsData statsData)
        {
            _signalBus = signalBus;
            _statsData = statsData;

            InitStats();
        }
        
        public void Reset()
        {
            InitStats();
            _signalBus.TryFire(new PlayerStatsResetSignal { Stats = _currentStats });
        }

        public void EnemyAttackedHandler(EnemyCrossedFinishLine enemyCrossedFinishLine)
        {
            _currentStats.DecreaseHealth(enemyCrossedFinishLine.Damage);
            
            if (_currentStats.IsDied)
            {
                _signalBus.TryFire(new PlayerDiedSignal {Score = _currentStats.Score});
            }
        }

        public void EnemyKilledHandler(EnemyDiedSignal enemyDiedSignal)
        {
            _currentStats.AddScore(enemyDiedSignal.Score);
            _currentStats.AddExperience(enemyDiedSignal.Experience);

            var levelsUp = _currentStats.LevelUp(_statsData);
            
            if (levelsUp > 0)
            {
                _signalBus.TryFire(new PlayerLeveledUpSignal { Level = _currentStats.Level });
            }
            
            _signalBus.TryFire(new PlayerScoreUpdatedSignal{Score = _currentStats.Score});
        }
        
        private void InitStats()
        {
            _currentStats = new Stats
            {
                Health = _statsData.Health,
                Level = 1,
                Score = 0,
                Experience = 0,
            };

            _currentStats.Damage = _currentStats.GetDamage(_statsData.Damage);
        }
    }
}