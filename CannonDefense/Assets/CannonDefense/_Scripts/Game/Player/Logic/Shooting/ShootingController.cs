using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    public sealed class ShootingController : IShootingController
    {
        private readonly IInputManager _inputManager;
        private readonly IGlassyObjectPool<CannonBall> _cannonBallPool;
        
        private Transform _cannonBallsParent;
        private bool _canShoot;
        
        public ShootingController(IInputManager inputManager, ShootingData data, CannonBall.Factory cannonBallFactory, Transform cannonBallSpawnPoint)
        {
            _inputManager = inputManager;
            
            _cannonBallsParent = new GameObject(nameof(_cannonBallsParent)).transform;
            _cannonBallPool = new CannonBallPool(data, cannonBallFactory, cannonBallSpawnPoint, _cannonBallsParent, data.CannonBall);
        }

        public void Dispose()
        {
            Disable();
        }

        public void Enable()
        {
            _inputManager.OnSpacePressed += Shoot;
            _canShoot = true;
        }

        public void Disable()
        {
            _canShoot = false;
            _inputManager.OnSpacePressed -= Shoot;
        }

        public void ResetCannonBalls()
        {
            Object.Destroy(_cannonBallsParent.gameObject);
            _cannonBallsParent = new GameObject(nameof(_cannonBallsParent)).transform;
            _cannonBallPool.SetPoolParent(_cannonBallsParent);
            _cannonBallPool.Clear();
        }
        
        private void Shoot()
        {
            if (!_canShoot) return;

            _cannonBallPool.Pool.Get();
        }
    }
}