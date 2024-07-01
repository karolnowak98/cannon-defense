using System;
using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Data;
using GlassyCode.CannonDefense.Game.Player.Logic.Movement;
using GlassyCode.CannonDefense.Game.Player.Logic.Shooting;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills;
using GlassyCode.CannonDefense.Game.Player.Logic.Stats;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic
{
    public class PlayerManager : IPlayerManager, IDisposable, ITickable, IFixedTickable
    {
        private SignalBus _signalBus;
        
        public IStatsController Stats { get; private set; }
        public ISkillsController Skills { get; private set; }
        private IMovementController Movement { get; set; }
        private IShootingController Shooting { get; set; }

        [Inject]
        private void Construct(SignalBus signalBus, IInputManager inputManager, IPlayerConfig config, 
            Transform player, Rigidbody rb, CannonBall.Factory cannonBallFactory, Transform cannonBallSpawnPoint, 
            OffensiveSkillProjectile.Factory skillProjectile)
        {
            _signalBus = signalBus;
            
            Stats = new StatsController(signalBus, config.Stats);
            Movement = new MovementController(inputManager, player, rb, config.Movement);
            Shooting = new ShootingController(inputManager, config.Shooting, cannonBallFactory, cannonBallSpawnPoint);
            Skills = new SkillsController(signalBus, config.OffensiveSkills, skillProjectile, cannonBallSpawnPoint);
            
            _signalBus.Subscribe<EnemyCrossedFinishLineSignal>(Stats.EnemyAttackedHandler);
            _signalBus.Subscribe<EnemyDiedSignal>(Stats.EnemyKilledHandler);
        }
        
        public void Dispose()
        {
            Movement.Dispose();
            Skills.Dispose();
            Shooting.Dispose();
            
            _signalBus.TryUnsubscribe<EnemyCrossedFinishLineSignal>(Stats.EnemyAttackedHandler);
            _signalBus.TryUnsubscribe<EnemyDiedSignal>(Stats.EnemyKilledHandler);
        }

        public void Tick()
        {
            Movement.Tick();
            Skills.Tick();
        }

        public void FixedTick()
        {
            Movement.FixedTick();
        }
        
        public void EnableControls()
        {
            Movement.Enable();
            Shooting.Enable();
            Skills.Enable();
        }
        
        public void DisableControls()
        {
            Movement.Disable();
            Shooting.Disable();
            Skills.Disable();
        }

        public void Reset()
        {
            Stats.Reset();
            Movement.ResetPosition();
            Shooting.ResetCannonBalls();
            Skills.Reset();
            _signalBus.TryFire<PlayerResetSignal>();
        }
    }
}