using System;
using GlassyCode.CannonDefense.Core.Input;
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
        private Transform _player;
        private Rigidbody _rb;
        
        public IPlayerConfig PlayerConfig { get; private set; }
        public IMovementController MovementController { get; private set; }
        public IShootingController ShootingController { get; private set; }
        public IStatsController StatsController { get; private set; }
        
        [Inject]
        private void Construct(IInputManager inputManager, IPlayerConfig playerConfig, Transform player, Rigidbody rb)
        {
            _player = player;
            _rb = rb;

            MovementController = new MovementController(inputManager);
            ShootingController = new ShootingController(inputManager);
            StatsController = new StatsController();
        }
        
        public void Dispose()
        {
            MovementController.Dispose();
            ShootingController.Dispose();
        }

        public void Tick()
        {
            ShootingController.Tick();
            MovementController.Tick();
        }

        public void FixedTick()
        {
            MovementController.FixedTick();
        }

        public void Reset()
        {
            StatsController.Reset();
        }
    }
}