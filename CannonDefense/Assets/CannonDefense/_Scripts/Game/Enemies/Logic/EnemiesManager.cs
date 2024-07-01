using System;
using System.Collections.Generic;
using System.Linq;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemiesManager : IEnemiesManager, ITickable, IDisposable
    {
        private readonly HashSet<IEnemy> _enemies = new();
        private SignalBus _signalBus;
        
        public IQuadtree Quadtree { get; private set; }
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(SignalBus signalBus, IQuadtree quadtree, ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            _signalBus = signalBus;
            Quadtree = quadtree;
            Spawner = new EnemySpawner(signalBus, timeController, config, factory, spawningArea);
            
            Spawner.OnRemovedEnemies += RemoveEnemies;
            _signalBus.Subscribe<EnemySpawnedSignal>(AddEnemy);
            _signalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            _signalBus.Subscribe<EnemyWoundedSignal>(OnEnemyWounded);
            _signalBus.Subscribe<EnemyCrossedFinishLineSignal>(OnEnemyCrossedFinishLine);
            _signalBus.Subscribe<SkillProjectileBoomedSignal>(TakeDamageIfInRange);
            
            Quadtree.Reset();
        }
        
        public void Dispose()
        {
            Spawner.OnRemovedEnemies -= RemoveEnemies;
            _signalBus.TryUnsubscribe<EnemySpawnedSignal>(AddEnemy);
            _signalBus.TryUnsubscribe<EnemyDiedSignal>(OnEnemyDied);
            _signalBus.TryUnsubscribe<EnemyWoundedSignal>(OnEnemyWounded);
            _signalBus.TryUnsubscribe<EnemyCrossedFinishLineSignal>(OnEnemyCrossedFinishLine);
            _signalBus.TryUnsubscribe<SkillProjectileBoomedSignal>(TakeDamageIfInRange);
        }
        
        public void Tick()
        {
            Spawner.Tick();
        }
        
        private void AddEnemy(EnemySpawnedSignal signal)
        {
            _enemies.Add(signal.Enemy);
        }

        private void RemoveEnemies()
        {
            _enemies.Clear();
            Quadtree.Reset();
        }
        
        private void OnEnemyCrossedFinishLine(EnemyCrossedFinishLineSignal signal)
        {
            foreach (var enemy in _enemies)
            {
                enemy.UpdateStatsByEffects(EnemyEffectTrigger.CrossedFinishLine, signal.Effects);
            }
        }

        private void OnEnemyWounded(EnemyWoundedSignal signal) 
        {
            foreach (var enemy in _enemies)
            {
                enemy.UpdateStatsByEffects(EnemyEffectTrigger.Wounded, signal.Effects);
            }
        }

        private void OnEnemyDied(EnemyDiedSignal signal)
        {
            foreach (var enemy in _enemies)
            {
                enemy.UpdateStatsByEffects(EnemyEffectTrigger.Died, signal.Effects);
            }
        }
        
        private void TakeDamageIfInRange(SkillProjectileBoomedSignal signal)
        {
            var location = new Vector2(signal.ExplosionCenter.x, signal.ExplosionCenter.z);
            var elements = Quadtree.GetElementsInRange(location, signal.Radius);

            foreach (var enemy in elements.Cast<IEnemy>())
            {
                enemy.TakeDamage(signal.Damage);
            }
        }
    }
}