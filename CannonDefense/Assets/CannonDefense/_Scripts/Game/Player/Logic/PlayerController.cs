using System;
using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Data;
using GlassyCode.CannonDefense.Game.Player.Logic.Movement;
using GlassyCode.CannonDefense.Game.Player.Logic.Shooting;
using GlassyCode.CannonDefense.Game.Player.Logic.Stats;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerController : IPlayerController, IDisposable, ITickable, IFixedTickable
    {
        private SignalBus _signalBus;
        
        public IPlayerConfig Config { get; private set; }
        public IStatsController Stats { get; private set; }
        public IMovementController Movement { get; private set; }
        public IShootingController Shooting { get; private set; }
        
        [Inject]
        private void Construct(SignalBus signalBus, IInputManager inputManager, IPlayerConfig config, Transform transform, Rigidbody rb, Transform cannonBallSpawnPoint)
        {
            _signalBus = signalBus;
            Config = config;
            Stats = new StatsController();
            Movement = new MovementController(inputManager, transform, rb, config.Movement);
            Shooting = new ShootingController(inputManager, config.Shooting, cannonBallSpawnPoint);
            
            _signalBus.Subscribe<EnemyCrossFinishLineSignal>(enemy => Stats.TakeDamage(enemy.Damage));
            
            EnableControls();
        }
        
        public void Dispose()
        {
            //_signalBus.TryUnsubscribe<EnemyCrossFinishLineSignal>();
            Movement.Dispose();
        }

        public void Tick()
        {
            Movement.Tick();
        }

        public void FixedTick()
        {
            Movement.FixedTick();
        }
        
        public void EnableControls()
        {
            Movement.Enable();
            Shooting.Enable();
        }
        
        public void DisableControls()
        {
            Movement.Disable();
            Shooting.Disable();
        }

        public void Reset()
        {
            Stats.Reset();
            Movement.ResetPosition();
        }
    }
}