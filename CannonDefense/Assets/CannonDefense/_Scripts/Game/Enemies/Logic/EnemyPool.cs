using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemyPool : GlassyObjectRandomPool<Enemy>, IEnemyPool
    {
        private readonly Enemy.Factory _factory;
        private readonly BoxCollider _spawningArea;
        
        public EnemyPool(IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea) : base(config.Enemies, config.EnemyPoolInitialSize, config.EnemyPoolMaxSize)
        {
            _factory = factory;
            _spawningArea = spawningArea;
        }

        protected override Enemy CreateElement()
        {
            var enemy = _factory.Create(Prefabs.GetRandomElement());
            enemy.Pool = Pool;
            enemy.SetPosition(_spawningArea.GetRandomPointInCollider());
            enemy.Reset();
            return enemy;
        }

        protected override void OnGetElementFromPool(Enemy element)
        {
            element.SetPosition(_spawningArea.GetRandomPointInCollider());
            base.OnGetElementFromPool(element);
        }
    }
}