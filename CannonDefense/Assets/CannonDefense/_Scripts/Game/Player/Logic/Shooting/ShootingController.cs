using GlassyCode.CannonDefense.Core.Input;
using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using GlassyCode.CannonDefense.Game.Player.Logic.CannonBall;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    public class ShootingController : IShootingController
    {
        private readonly IInputManager _inputManager;
        private readonly IGlassyObjectPool<CannonBall.CannonBall> _cannonBallPool;
        
        private bool _canShoot;
        
        public ShootingController(IInputManager inputManager, ShootingData data, Transform cannonBallSpawnPoint)
        {
            _inputManager = inputManager;
            
            _cannonBallPool = new CannonBallPool(data, cannonBallSpawnPoint);
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
        
        private void Shoot()
        {
            if (!_canShoot) return;

            _cannonBallPool.Pool.Get();
        }
    }
}