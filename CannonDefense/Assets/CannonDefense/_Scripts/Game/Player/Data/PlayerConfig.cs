using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Skills.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(PlayerConfig), fileName = nameof(PlayerConfig))]
    public class PlayerConfig : Config, IPlayerConfig
    {
        [field: SerializeField] public StatsData Stats { get; private set; }
        [field: SerializeField] public ShootingData Shooting { get; private set; }
        [field: SerializeField] public TransformData Transform { get; private set; }
        [field: SerializeField] public MovementData Movement { get; private set; }
        [field: SerializeField] public OffensiveSkillEntityData[] OffensiveSkills { get; private set; }
    }
}