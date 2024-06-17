using System;
using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly Dictionary<EnemyType, IGlassyObjectPool<Enemy>> _enemyPools = new();
        private readonly ITimer _timer;

        public EnemySpawner(ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                var enemy = config.GetEnemyByType(type);
                
                if(enemy == null) continue;
                
                _enemyPools[type] = new EnemyPool(config, factory, spawningArea, enemy);
            }
            
            _timer = new AutomaticTimer(timeController,  config.SpawnInterval);
        }

        public void Tick()
        {
            _timer.Tick();
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

        public void ClearPools()
        {
            foreach (var enemyPool in _enemyPools.Values)
            {
                enemyPool.Clear();
            }
        }
        
        private void SpawnEnemy()
        {
            _enemyPools[EnumExtensions.GetRandomValue<EnemyType>()].Pool.Get();
        }
    }
}