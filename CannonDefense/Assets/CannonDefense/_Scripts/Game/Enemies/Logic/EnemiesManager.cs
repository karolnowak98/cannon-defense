using System;
using System.Linq;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic;
using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemiesManager : IEnemiesManager, ITickable, IDisposable
    {
        private SignalBus _signalBus;
        private IQuadtree _quadtree;
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(SignalBus signalBus, IQuadtree quadtree, ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            _quadtree = quadtree;
            _signalBus = signalBus;
            Spawner = new EnemySpawner(timeController, config, factory, spawningArea);
            
            _signalBus.Subscribe<SkillProjectileBoomedSignal>(TakeDamageIfInRange);
        }
        
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<SkillProjectileBoomedSignal>(TakeDamageIfInRange);
        }
        
        public void Tick()
        {
            Spawner.Tick();
        }
        
        private void TakeDamageIfInRange(SkillProjectileBoomedSignal signal)
        {
            var location = new Vector2(signal.ExplosionCenter.x, signal.ExplosionCenter.z);
            var elements = _quadtree.GetElementsInRange(location, signal.Radius);

            foreach (var enemy in elements.Cast<IEnemy>())
            {
                enemy.TakeDamage(signal.Damage);
            }
        }
    }
}