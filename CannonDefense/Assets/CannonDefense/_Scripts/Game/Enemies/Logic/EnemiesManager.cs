using System;
using System.Linq;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemiesManager : IEnemiesManager, ITickable, IDisposable
    {
        private SignalBus _signalBus;
        
        public IEnemyGrid Grid { get; private set; }
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(SignalBus signalBus, ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            _signalBus = signalBus;
            
            Spawner = new EnemySpawner(signalBus, timeController, config, factory, spawningArea);
            Grid = new EnemyGrid(4);
            
            _signalBus.Subscribe<EnemySpawnedSignal>(AddEnemy);
            _signalBus.Subscribe<EnemyCrossedFinishLine>(RemoveEnemy);
            _signalBus.Subscribe<EnemyDiedSignal>(RemoveEnemy);
            _signalBus.Subscribe<SkillProjectileBoomedSignal>(OnSkillBoomed);
        }
        
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<EnemySpawnedSignal>(AddEnemy);
            _signalBus.TryUnsubscribe<EnemyCrossedFinishLine>(RemoveEnemy);
            _signalBus.TryUnsubscribe<EnemyDiedSignal>(RemoveEnemy);
            _signalBus.TryUnsubscribe<SkillProjectileBoomedSignal>(OnSkillBoomed);
        }
        
        public void Tick()
        {
            Spawner.Tick();
            
            foreach (var enemy in Grid.GetAllEnemies())
            {
                Grid.UpdateEnemyPosition(enemy);
            }
        }
        
        private void OnSkillBoomed(SkillProjectileBoomedSignal signal)
        {
            var enemiesInRange = Grid.GetEnemiesInRange(signal.ExplosionCenter, signal.Radius);

            foreach (var enemy in enemiesInRange)
            {
                var distance = Vector3.Distance(enemy.Transform.position, signal.ExplosionCenter);
                if (distance <= signal.Radius * signal.Radius)
                {
                    enemy.TakeDamage(signal.Damage);
                }
            }
        }
        
        private void AddEnemy(EnemySpawnedSignal signal)
        {
            Grid.AddEnemy(signal.Enemy);
        }
        
        private void RemoveEnemy(EnemyCrossedFinishLine signal)
        {
            Grid.RemoveEnemy(signal.Enemy);
        }
        
        private void RemoveEnemy(EnemyDiedSignal signal)
        {
            Grid.RemoveEnemy(signal.Enemy);
        }
    }
}