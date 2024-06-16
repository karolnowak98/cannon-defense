using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.CannonBall
{
    public class CannonBallPool : GlassyObjectPool<CannonBall>
    {
        private readonly Transform _spawnPoint;
        
        public CannonBallPool(ShootingData data, Transform cannonBallSpawnPoint) : base(data.CannonBall, data.CannonBallPoolInitialSize, data.CannonBallPoolMaxSize)
        {
            _spawnPoint = cannonBallSpawnPoint;
        }

        protected override CannonBall CreateElement()
        {
            var cannonBall = base.CreateElement(); 
            cannonBall.SetPosition(_spawnPoint.position);
            return cannonBall;
        }

        protected override void OnGetElementFromPool(CannonBall enemy)
        {
            enemy.SetPosition(_spawnPoint.position);
            base.OnGetElementFromPool(enemy);
        }
    }
}