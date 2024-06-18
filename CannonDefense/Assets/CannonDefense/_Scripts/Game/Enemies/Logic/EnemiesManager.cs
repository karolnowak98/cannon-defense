using System;
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
        
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(SignalBus signalBus, ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            Spawner = new EnemySpawner(timeController, config, factory, spawningArea);
            
            _signalBus = signalBus;
            _signalBus.Subscribe<SkillProjectileBoomedSignal>(RemoveEnemies);
        }
        
        public void Dispose()
        {
            _signalBus.TryUnsubscribe<SkillProjectileBoomedSignal>(RemoveEnemies);
        }
        
        public void Tick()
        {
            Spawner.Tick();
        }

        private void RemoveEnemies(SkillProjectileBoomedSignal signal)
        {
            
        }
    }
}