using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility.Static;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(EnemiesConfig), fileName = nameof(EnemiesConfig))]
    public class EnemiesConfig : Config, IEnemiesConfig
    {
        [field: Header("Spawner settings."), Tooltip("Prefab of Enemy with the related component.")]
        [field: SerializeField] public Enemy Enemy { get; private set; }
        
        [field: Tooltip("Enemy spawn interval. ")]
        [field: SerializeField] public float SpawnInterval { get; private set; }
        
        [field: Tooltip("Spawning area")]
        [field: SerializeField] public Transform SpawningArea { get; private set; }
        
        [field: Header("Enemy Pool"), Tooltip("Default (initial) enemy pool size.")]
        [field: SerializeField] public int EnemyPoolInitialSize { get; private set; }
        
        [field: Tooltip("Max enemy pool size. Make sure that pool won't reach that value.")]
        [field: SerializeField] public int EnemyPoolMaxSize { get; private set; }
    }
}