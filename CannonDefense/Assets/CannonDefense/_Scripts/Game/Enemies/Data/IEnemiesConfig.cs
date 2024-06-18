using GlassyCode.CannonDefense.Game.Enemies.Enums;
using GlassyCode.CannonDefense.Game.Enemies.Logic;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    public interface IEnemiesConfig
    {
        float SpawnInterval { get; }
        int EnemyPoolInitialSize { get; }
        int EnemyPoolMaxSize { get; }
        Enemy GetEnemyByType(EnemyType enemyType);
        EnemyType GetRandomEnemyName();
    }
}