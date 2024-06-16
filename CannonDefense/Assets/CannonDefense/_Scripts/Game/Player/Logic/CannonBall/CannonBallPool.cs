using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using UnityEngine;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Game.Player.Logic.CannonBall
{
    public class CannonBallPool : ICannonBallPool
    {
        private readonly CannonBall _cannonBall;
        private readonly Transform _spawnPoint;
        
        public ObjectPool<CannonBall> Pool { get; }
        
        public CannonBallPool(ShootingData data, Transform cannonBallSpawnPoint)
        {
            _cannonBall = data.CannonBall;
            _spawnPoint = cannonBallSpawnPoint;
            
            Pool = new ObjectPool<CannonBall>(CreateProjectile, OnGetCannonBallFromPool, OnReleaseCannonBallToPool, OnDestroyCannonBall, true, data.CannonBallPoolInitialSize, data.CannonBallPoolMaxSize);
        }
        
        private CannonBall CreateProjectile()
        {
            //TODO check if Create will work
            var cannonBallMb = Object.Instantiate(_cannonBall);
            cannonBallMb.Pool = Pool;
            cannonBallMb.Reset(_spawnPoint.position);
            return cannonBallMb;
        }
        
        private void OnGetCannonBallFromPool(CannonBall cannonBall)
        {
            cannonBall.Reset(_spawnPoint.position);
        }

        private void OnReleaseCannonBallToPool(CannonBall cannonBall)
        {
            cannonBall.Disable();
        }

        private void OnDestroyCannonBall(CannonBall cannonBall)
        {
            cannonBall.Destroy();
        }
    }
}