using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly IGlassyObjectPool<Enemy> _enemyPool;
        private readonly ITimer _timer;

        public EnemySpawner(ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            _enemyPool = new EnemyPool(config, factory, spawningArea);
            _timer = new AutomaticTimer(timeController,  config.SpawnInterval);
        }

        public void Tick()
        {
            _timer.Tick();
        }

        private void SpawnEnemy()
        {
            _enemyPool.Pool.Get();
        }

        public void StartSpawning()
        {
            _timer.OnTimerExpired += SpawnEnemy;
            _timer.Start();
        }

        public void StopSpawning()
        {
            _timer.OnTimerExpired -= SpawnEnemy;
            _timer.Stop();
        }
    }
}