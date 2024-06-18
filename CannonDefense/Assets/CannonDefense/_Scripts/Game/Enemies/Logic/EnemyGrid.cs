using System.Collections.Generic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemyGrid : IEnemyGrid
    {
        private readonly Dictionary<Vector2Int, List<IEnemy>> _grid;
        private readonly float _cellSize;

        public EnemyGrid(float cellSize)
        {
            _cellSize = cellSize;
            _grid = new Dictionary<Vector2Int, List<IEnemy>>();
        }

        private Vector2Int GetCellIndex(Vector3 position)
        {
            return new Vector2Int(
                Mathf.FloorToInt(position.x / _cellSize),
                Mathf.FloorToInt(position.z / _cellSize)
            );
        }

        public void AddEnemy(IEnemy enemy)
        {
            var cellIndex = GetCellIndex(enemy.Transform.position);
            if (!_grid.ContainsKey(cellIndex))
            {
                _grid[cellIndex] = new List<IEnemy>();
            }
            _grid[cellIndex].Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
         {
            var cellIndex = GetCellIndex(enemy.Transform.position);
            
            if (!_grid.TryGetValue(cellIndex, out var value)) return;
            
            value.Remove(enemy);
            if (_grid[cellIndex].Count == 0)
            {
                _grid.Remove(cellIndex);
            }
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
                    if (_grid.TryGetValue(cellIndex, out var value))
                    {
                        enemiesInRange.AddRange(value);
                    }
                }
            }

            return enemiesInRange;
        }
    }
}