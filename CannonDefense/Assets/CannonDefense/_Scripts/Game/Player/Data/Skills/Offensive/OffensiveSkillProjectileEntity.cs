using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive
{
    [CreateAssetMenu(menuName = MenuNames.Entities + nameof(OffensiveSkillProjectileEntity), fileName = nameof(OffensiveSkillProjectileEntity))]
    public class OffensiveSkillProjectileEntity : EntityData
    {
        [field: Tooltip("Speed of the projectile.")]
        [field: SerializeField] public float Speed { get; private set; } = 5f;
    }
}