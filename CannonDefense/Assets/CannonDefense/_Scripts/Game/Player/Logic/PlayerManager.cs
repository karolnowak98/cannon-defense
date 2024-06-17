using System;
using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Data;
using GlassyCode.CannonDefense.Game.Player.Logic.Movement;
using GlassyCode.CannonDefense.Game.Player.Logic.Shooting;
using GlassyCode.CannonDefense.Game.Player.Logic.Stats;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerManager : IPlayerManager, IDisposable, ITickable, IFixedTickable
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
            
            Stats = new StatsController(config.Stats);
            Movement = new MovementController(inputManager, transform, rb, config.Movement);
            Shooting = new ShootingController(inputManager, config.Shooting, cannonBallSpawnPoint);
            
            _signalBus.Subscribe<EnemyAttackedSignal>(Stats.EnemyAttackedHandler);
            _signalBus.Subscribe<EnemyKilledSignal>(Stats.EnemyKilledHandler);

            Reset();
        }
        
        public void Dispose()
        {
            Movement.Dispose();
            
            _signalBus.TryUnsubscribe<EnemyAttackedSignal>(Stats.EnemyAttackedHandler);
            _signalBus.TryUnsubscribe<EnemyKilledSignal>(Stats.EnemyKilledHandler);
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