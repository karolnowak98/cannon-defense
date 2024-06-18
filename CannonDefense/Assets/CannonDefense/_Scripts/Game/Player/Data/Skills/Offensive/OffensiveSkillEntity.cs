using GlassyCode.CannonDefense.Core.Editors;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive
{
    [CreateAssetMenu(menuName = MenuNames.Skills + nameof(OffensiveSkillEntity), fileName = nameof(OffensiveSkillEntity))]
    public class OffensiveSkillEntity : SkillEntity, IOffensiveSkillEntity
    {
        [field: SerializeField, Header("Offensive Stats")] public OffensiveSkillProjectile OffensiveSkillProjectile { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsDamageScalingByLevel { get; private set; }
        [field: SerializeField, Tooltip("The levels are sorted from first to the highest.")] public int[] Damage { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsAreaScalingByLevel { get; private set; }
        [field: SerializeField, Tooltip("The levels are sorted from first to the highest.")] public float[] Area { get; private set; } 
        
        protected override void OnValidate()
        {
            base.OnValidate();

            if (Damage is not null && Damage.Length > 0) IsDamageScalingByLevel = Damage.Length > 1;
            if (Area is not null && Area.Length > 0) IsAreaScalingByLevel = Area.Length > 1;
        }
    }
}