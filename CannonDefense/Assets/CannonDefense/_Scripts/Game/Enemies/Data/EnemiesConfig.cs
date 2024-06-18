using System;
using System.Collections.Generic;
using System.Linq;
using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(EnemiesConfig), fileName = nameof(EnemiesConfig))]
    public sealed class EnemiesConfig : Config, IEnemiesConfig
    {
        [field: Header("Spawner settings."), Tooltip("Prefab of Enemy with the related component. Make sure for each name there is only one enemy.")]
        [field: SerializeField] public EnemyData[] Enemies { get; private set; }
        
        [field: Tooltip("Enemy spawn interval in seconds. ")]
        [field: SerializeField] public float SpawnInterval { get; private set; }
        
        [field: Header("Enemy Pools"), Tooltip("Default (initial) enemy pool size.")]
        [field: SerializeField] public int EnemyPoolInitialSize { get; private set; }
        
        [field: Tooltip("Max enemy pool size. Make sure that pool won't reach that value.")]
        [field: SerializeField] public int EnemyPoolMaxSize { get; private set; }
        
        //TODO consider referencing GameObject instead (because dependencies)
        //For that case might be better using serialized Dictionary with Odin inspector
        public Enemy GetEnemyByType(EnemyType enemyType) => Enemies.FirstOrDefault(e => e.Enemy.Type == enemyType).Enemy;

        public EnemyType GetRandomEnemyName()
        {
            var randomPoint = UnityEngine.Random.value * 100;
            float total = 0;

            foreach (var enemy in Enemies)
            {
                total += enemy.SpawnRatio;
                
                if (randomPoint <= total)
                {
                    return enemy.Enemy.Type;
                }
            }

            return Enemies[0].Enemy.Type;
        }

        private void OnValidate()
        {
            ValidateSpawnRatio();
            ValidateUniqueEnemyNames();
            ValidateEnemiesCount();
        }

        private void ValidateSpawnRatio()
        {
            var totalSpawnRatio = Enemies.Sum(e => e.SpawnRatio);

            if (Math.Abs(totalSpawnRatio - 100f) > 0.01f)
            {
                Debug.LogError($"Total spawn ratio must be exactly 100%. Current total is {totalSpawnRatio}%.");
            }
            
            foreach (var enemy in Enemies)
            {
                if (enemy.SpawnRatio is < 0 or > 100)
                {
                    Debug.LogError($"Spawn ratio for {enemy.Enemy.name} must be between 0 and 100. Current value is {enemy.SpawnRatio}.");
                }
            }
        }
        
        private void ValidateUniqueEnemyNames()
        {
            var enemyNames = new HashSet<EnemyType>();
            
            foreach (var enemy in Enemies)
            {
                if (!enemyNames.Add(enemy.Enemy.Type))
                {
                    Debug.LogError($"Duplicate enemy type found: {enemy.Enemy.Type}. Enemy types must be unique.");
                }
            }
        }

        private void ValidateEnemiesCount()
        {
            var enumEnemyNameLength = Enum.GetValues(typeof(EnemyType)).Length;
            var enemiesNumber = Enemies.Length;

            if (enumEnemyNameLength != enemiesNumber)
            {
                Debug.LogError($"Mismatch in enemy type count. Enum defines {enumEnemyNameLength} types, but config contains {enemiesNumber} entries.");
            }
        }
    }
}