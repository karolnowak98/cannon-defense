using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemyGrid
    {
        void AddEnemy(IEnemy enemy);
        void RemoveEnemy(IEnemy enemy);
        void UpdateEnemyPosition(IEnemy enemy);
        List<IEnemy> GetEnemiesInRange(Vector3 position, float radius);
        List<IEnemy> GetAllEnemies();
    }
}