using System;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemySpawner
    {
        event Action<IEnemy> OnSpawnedEnemy;
        event Action OnRemovedEnemies;
        void Tick();
        void StartSpawning();
        void StopSpawning();
        void RemoveEnemies();
    }
}