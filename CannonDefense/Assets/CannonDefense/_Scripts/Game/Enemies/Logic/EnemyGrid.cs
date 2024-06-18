using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemyGrid : IEnemyGrid
    {
        private readonly Dictionary<Vector2Int, List<IEnemy>> _grid;
        private readonly float _cellSize;
        private readonly Dictionary<IEnemy, Vector2Int> _enemyPositions;

        public EnemyGrid(float cellSize)
        {
            _cellSize = cellSize;
            _grid = new Dictionary<Vector2Int, List<IEnemy>>();
            _enemyPositions = new Dictionary<IEnemy, Vector2Int>();
        }

        public void AddEnemy(IEnemy enemy)
        {
            var cellIndex = GetCellIndex(enemy.Transform.position);
            if (!_grid.TryGetValue(cellIndex, out var enemies))
            {
                enemies = new List<IEnemy>();
                _grid[cellIndex] = enemies;
            }
            enemies.Add(enemy);
            _enemyPositions[enemy] = cellIndex;
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            if (!_enemyPositions.TryGetValue(enemy, out var cellIndex))
                return;

            if (!_grid.TryGetValue(cellIndex, out var enemies))
                return;

            enemies.Remove(enemy);
            _enemyPositions.Remove(enemy);

            if (enemies.Count == 0)
                _grid.Remove(cellIndex);
        }

        public void UpdateEnemyPosition(IEnemy enemy)
        {
            if (!_enemyPositions.TryGetValue(enemy, out var oldCellIndex)) return;
            if (enemy.Transform == null) return;

            var newCellIndex = GetCellIndex(enemy.Transform.position);

            if (oldCellIndex == newCellIndex)
                return;

            RemoveEnemy(enemy);
            AddEnemy(enemy);
        }

        public List<IEnemy> GetEnemiesInRange(Vector3 position, float radius)
        {
            var enemiesInRange = new List<IEnemy>();
            var cellRadius = Mathf.CeilToInt(radius / _cellSize);
            var centerCell = GetCellIndex(position);

            for (var x = -cellRadius; x <= cellRadius; x++)
            {
                for (var z = -cellRadius; z <= cellRadius; z++)
                {
                    var cellIndex = new Vector2Int(centerCell.x + x, centerCell.y + z);
                    if (_grid.TryGetValue(cellIndex, out var enemies))
                    {
                        enemiesInRange.AddRange(enemies);
                    }
                }
            }

            return enemiesInRange;
        }
        
        public List<IEnemy> GetAllEnemies()
        {
            return _enemyPositions.Keys.ToList();
        }
        
        private Vector2Int GetCellIndex(Vector3 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x / _cellSize),
                Mathf.FloorToInt(position.z / _cellSize)
            );
        }
    }
}