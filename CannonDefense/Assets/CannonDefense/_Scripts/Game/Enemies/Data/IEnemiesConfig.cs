using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    public interface IEnemiesConfig
    {
        Enemy[] Enemies { get; }
        float SpawnInterval { get; }
        int EnemyPoolInitialSize { get; }
        int EnemyPoolMaxSize { get; }
    }
}