using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    public class CannonBallPool : GlassyObjectPool<CannonBall>
    {
        private readonly CannonBall.Factory _factory;
        private readonly Transform _spawnPoint;
        private readonly CannonBall _cannonBall;
        
        public CannonBallPool(ShootingData data, CannonBall.Factory factory, Transform cannonBallSpawnPoint, Transform parent, CannonBall cannonBall) 
            : base(data.CannonBall, parent, data.CannonBallPoolInitialSize, data.CannonBallPoolMaxSize)
        {
            _factory = factory;
            _spawnPoint = cannonBallSpawnPoint;
            _cannonBall = cannonBall;
        }

        protected override CannonBall CreateElement()
        {
            var cannonBall = _factory.Create(_cannonBall);
            cannonBall.Pool = Pool;
            cannonBall.SetPosition(_spawnPoint.position);
            cannonBall.SetParent(Parent);
            return cannonBall;
        }

        protected override void OnGetElementFromPool(CannonBall enemy)
        {
            enemy.SetPosition(_spawnPoint.position);
            base.OnGetElementFromPool(enemy);
        }
    }
}