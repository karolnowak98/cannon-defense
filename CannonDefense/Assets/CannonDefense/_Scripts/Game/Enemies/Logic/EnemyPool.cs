using GlassyCode.CannonDefense.Core.Pool;
using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemyPool : GlassyObjectPool<Enemy>, IEnemyPool
    {
        public EnemyPool(IEnemiesConfig config) : base(config.Enemy, config.EnemyPoolInitialSize, config.EnemyPoolMaxSize)
        {
        }
    }
}