using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Skills.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [CreateAssetMenu(menuName = MenuNames.Configs + nameof(PlayerConfigData), fileName = nameof(PlayerConfigData))]
    public class PlayerConfigData : ConfigData, IPlayerConfig
    {
        [field: SerializeField] public PlayerStats BaseStats { get; private set; }
        [field: SerializeField] public OffensiveSkillEntityData[] OffensiveSkills { get; private set; }
    }
}