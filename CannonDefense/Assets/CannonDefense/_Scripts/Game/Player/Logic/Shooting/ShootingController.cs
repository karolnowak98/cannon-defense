using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Game.Player.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    public class ShootingController : IShootingController
    {
        private readonly IInputManager _inputManager;
        private readonly ShootingData _data;
        private readonly Transform _cannonBallSpawnPoint;
        
        private bool _canShoot;
        //private float _lastShotTime;
        
        //public event Action<IEnemy> OnEnemyHit;
        
        public ShootingController(IInputManager inputManager, ShootingData data, Transform cannonBallSpawnPoint)
        {
            _inputManager = inputManager;
            _data = data;
            _cannonBallSpawnPoint = cannonBallSpawnPoint;
        }
        
        public void EnableShooting()
        {
            _inputManager.OnSpacePressed += Shoot;
            _canShoot = true;
        }

        public void DisableShooting()
        {
            _canShoot = false;
            _inputManager.OnSpacePressed -= Shoot;
        }
        
        private void Shoot()
        {
            if (!_canShoot) return;
            
            Object.Instantiate(_data.ProjectilePrefab, _cannonBallSpawnPoint.position, Quaternion.identity);
        }
    }
}