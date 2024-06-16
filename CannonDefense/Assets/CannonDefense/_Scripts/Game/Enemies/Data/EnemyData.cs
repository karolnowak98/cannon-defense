using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility.Static;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [CreateAssetMenu(menuName = MenuNames.Entities + nameof(EnemyData), fileName = nameof(EnemyData))]
    public class EnemyData : EntityData, IEnemyData
    {
        [field: SerializeField] public EnemyName EnemyName { get; private set; }
        
        
        
        [field: Tooltip("The movement speed of the enemy.")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 10f;
        
        [field: Tooltip("Enemy health.")]
        [field: SerializeField] public float Health { get; private set; }
        
        [field: Tooltip("The number of points a player receives after killing an enemy.")]
        [field: SerializeField] public int Score { get; private set; }
        
        [field: Tooltip("The number of experience a player receives after killing an enemy.")]
        [field: SerializeField] public int Experience { get; private set; }
    }
}