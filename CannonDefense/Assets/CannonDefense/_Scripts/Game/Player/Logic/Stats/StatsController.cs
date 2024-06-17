using System;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Data.Stats;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public class StatsController : IStatsController
    {
        private readonly PlayerLevel[] _levelsData;
        private readonly int _initHealth;
        
        private int _health;
        private int _score;
        private int _experience;
        private int _level;
        
        public event Action<int> OnScoreUpdated;
        public event Action<int> OnLevelUp;
        public event Action OnPlayerDied;
        
        public StatsController(StatsData statsData)
        {
            _initHealth = statsData.Health;
            _levelsData = statsData.Levels;
        }

        public void Reset()
        {
            _health = _initHealth;
            _score = 0;
            _experience = 0;
            _level = 1;
        }

        public void EnemyAttackedHandler(EnemyAttackedSignal enemyAttackedSignal)
        {
            _health -= enemyAttackedSignal.Damage;
            CheckIfDied();
        }

        public void EnemyKilledHandler(EnemyKilledSignal enemyKilledSignal)
        {
            _score += enemyKilledSignal.Score;
            _experience += enemyKilledSignal.Experience;
            
            CheckForLevelUp();
            OnScoreUpdated?.Invoke(_score);
        }
        
        private void CheckForLevelUp()
        {
            while (_level < _levelsData.Length && _experience >= _levelsData[_level - 1].RequiredExpForLevelUp)
            {
                _experience -= _levelsData[_level - 1].RequiredExpForLevelUp;
                _level++;
                OnLevelUp?.Invoke(_level);
            }
        }

        private void CheckIfDied()
        {
            if (_health <= 0)
            {
                OnPlayerDied?.Invoke();
            }
        }
    }
}