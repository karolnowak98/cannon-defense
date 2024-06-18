using GlassyCode.CannonDefense.Game.Player.Data.Movement;
using GlassyCode.CannonDefense.Game.Player.Data.Shooting;
using GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive;
using GlassyCode.CannonDefense.Game.Player.Data.Stats;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    public interface IPlayerConfig
    {
        StatsData Stats { get; }
        MovementData Movement { get; }
        ShootingData Shooting { get; }
        OffensiveSkillEntity[] OffensiveSkills { get; }
    }
}