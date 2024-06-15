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

        public IPlayerConfig Config { get; private set; }
        public IMovementController MovementController { get; private set; }
        public IShootingController ShootingController { get; private set; }
        public IStatsController StatsController { get; private set; }
        
        [Inject]
        private void Construct(IInputManager inputManager, Transform player, Rigidbody rb, Transform cannonBallSpawnPoint, IPlayerConfig playerConfig)
        {
            _player = player;
            Config = playerConfig;

            MovementController = new MovementController(inputManager, rb, playerConfig.Movement);
            ShootingController = new ShootingController(inputManager, Config.Shooting, cannonBallSpawnPoint);
            StatsController = new StatsController();
            
            EnableControls();
        }
        
        public void Dispose()
        {
            MovementController.Dispose();
        }

        public void Tick()
        {
            MovementController.Tick();
        }

        public void FixedTick()
        {
            MovementController.FixedTick();
        }
        
        public void EnableControls()
        {
            ShootingController.EnableShooting();
            MovementController.EnableMovement();
        }
        
        public void DisableControls()
        {
            MovementController.DisableMovement();
            ShootingController.DisableShooting();
        }

        public void Reset()
        {
            ResetPlayerPosition();
            StatsController.Reset();
        }

        private void ResetPlayerPosition()
        {
            _player.position = Config.Transform.StartingPosition;
        }
    }
}