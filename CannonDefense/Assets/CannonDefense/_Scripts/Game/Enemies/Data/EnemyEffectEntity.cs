using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [CreateAssetMenu(menuName = MenuNames.Enemies + nameof(EnemyEffectEntity), fileName = nameof(EnemyEffectEntity))]
    public sealed class EnemyEffectEntity : EntityData
    {
        //With Odin I would add EnumPaging and ShowIf attributes depending on enums states and booleans
        [field: SerializeField] public EnemyEffectType EffectType { get; private set; }
        [field: SerializeField] public EnemyEffectTrigger EffectTrigger { get; private set; }
        [field: Tooltip("If percentage value then use (0,1) range.")]
        [field: SerializeField] public float EffectValue { get; private set; }
        [field: SerializeField] public bool AffectSelf { get; private set; }
        [field: SerializeField] public bool AffectOthers { get; private set; }
        [field: SerializeField] public EnemyType[] AffectedEnemyTypes { get; private set; }
    }
}