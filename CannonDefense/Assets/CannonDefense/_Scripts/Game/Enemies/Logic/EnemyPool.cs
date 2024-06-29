using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemyPool : GlassyObjectPool<Enemy>
    {
        private readonly Enemy.Factory _factory;
        private readonly BoxCollider _spawningArea;
        private readonly Enemy _enemy;
        private Transform _parent;
        
        public EnemyPool(IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea, Transform parent, Enemy enemy) 
            : base(enemy, parent, config.EnemyPoolInitialSize, config.EnemyPoolMaxSize)
        {
            _factory = factory;
            _spawningArea = spawningArea;
            _enemy = enemy;
        }

        protected override Enemy CreateElement()
        {
            var enemy = _factory.Create(_enemy);
            enemy.SetPosition(_spawningArea.GetRandomPointInCollider());
            enemy.SetParent(Parent);
            enemy.Pool = Pool;
            return enemy;
        }

        protected override void OnGetElementFromPool(Enemy enemy)
        {
            enemy.SetPosition(_spawningArea.GetRandomPointInCollider());
            base.OnGetElementFromPool(enemy);
        }
    }
}