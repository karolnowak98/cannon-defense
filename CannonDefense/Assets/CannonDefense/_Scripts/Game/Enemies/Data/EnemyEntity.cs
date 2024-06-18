using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [CreateAssetMenu(menuName = MenuNames.Enemies + nameof(EnemyEntity), fileName = nameof(EnemyEntity))]
    public sealed class EnemyEntity : EntityData
    {
        [field: SerializeField] public EnemyType Type { get; private set; }
        
        [field: Tooltip("The movement speed of the enemy.")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
        
        [field: Tooltip("GetEnemyByType health.")]
        [field: SerializeField] public int MaxHealth { get; private set; }
        
        [field: Tooltip("GetEnemyByType damage when cross finish line.")]
        [field: SerializeField] public int Damage { get; private set; }
        
        [field: Tooltip("The number of points a player receives after killing an enemy.")]
        [field: SerializeField] public int Score { get; private set; }
        
        [field: Tooltip("The number of experience a player receives after killing an enemy.")]
        [field: SerializeField] public int Experience { get; private set; }
        
        [field: Tooltip("Effects triggered for enemy.")]
        [field: SerializeField] public EnemyEffectEntity[] Effects { get; private set; }
    }
}