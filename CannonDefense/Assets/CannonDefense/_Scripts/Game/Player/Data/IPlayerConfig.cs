using GlassyCode.CannonDefense.Game.Skills.Data;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    public interface IPlayerConfig
    {
        StatsData Stats { get; }
        TransformData Transform { get; }
        MovementData Movement { get; }
        ShootingData Shooting { get; }
        OffensiveSkillEntityData[] OffensiveSkills { get; }
    }
}