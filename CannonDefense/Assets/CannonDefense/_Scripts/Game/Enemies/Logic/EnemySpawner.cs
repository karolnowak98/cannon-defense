using System;
using System.Collections.Generic;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemySpawner : IEnemySpawner
    {
        private readonly Dictionary<EnemyType, IGlassyObjectPool<Enemy>> _enemyPools = new();
        private readonly IEnemiesConfig _config;
        private readonly ITimer _timer;
        private Transform _spawningEnemyParent;

        public event Action<IEnemy> OnSpawnedEnemy;
        public event Action OnRemovedEnemies;

        public EnemySpawner(ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            _config = config;
            _spawningEnemyParent = new GameObject(nameof(_spawningEnemyParent)).transform;
            _timer = new AutomaticTimer(timeController, config.SpawnInterval);

            InitPools(config, factory, spawningArea, _spawningEnemyParent);
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

        public void RemoveEnemies()
        {
            Object.Destroy(_spawningEnemyParent.gameObject);
            _spawningEnemyParent = new GameObject(nameof(_spawningEnemyParent)).transform; 
            
            foreach (var pool in _enemyPools.Values)
            {
                pool.Clear();
                pool.SetPoolParent(_spawningEnemyParent);
            }
        }
        
        private void InitPools(IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea, Transform spawningEnemyParent)
        {
            foreach (EnemyType type in Enum.GetValues(typeof(EnemyType)))
            {
                var enemy = config.GetEnemyByType(type);
                
                if(enemy == null) continue;
                
                _enemyPools[type] = new EnemyPool(config, factory, spawningArea, spawningEnemyParent, enemy);
            }
        }
        
        private void SpawnEnemy()
        {
             var enemy = _enemyPools[_config.GetRandomEnemyName()].Pool.Get();
             OnSpawnedEnemy?.Invoke(enemy);
        }
    }
}