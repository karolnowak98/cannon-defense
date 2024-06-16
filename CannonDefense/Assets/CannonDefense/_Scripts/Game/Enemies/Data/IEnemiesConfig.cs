using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    public interface IEnemiesConfig
    {
        Enemy Enemy { get; }
        float SpawnInterval { get; }
        Transform SpawningArea { get; }
        int EnemyPoolInitialSize { get; }
        int EnemyPoolMaxSize { get; }
    }
}