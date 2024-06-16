using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility.Static;
using GlassyCode.CannonDefense.Game.Player.Data.Movement;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using GlassyCode.CannonDefense.Game.Player.Data.Skills;
using GlassyCode.CannonDefense.Game.Player.Data.Stats;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(PlayerConfig), fileName = nameof(PlayerConfig))]
    public class PlayerConfig : Config, IPlayerConfig
    {
        [field: SerializeField] public StatsData Stats { get; private set; }
        [field: SerializeField] public MovementData Movement { get; private set; }
        [field: SerializeField] public ShootingData Shooting { get; private set; }
        [field: SerializeField] public OffensiveSkillData[] OffensiveSkills { get; private set; }
    }
}