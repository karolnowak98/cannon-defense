using GlassyCode.CannonDefense.Game.Player.Logic.Skills;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive
{
    public interface IOffensiveSkillEntity : ISkillEntity
    {
        OffensiveSkillProjectile OffensiveSkillProjectile  { get; }
        bool IsDamageScalingByLevel { get; }
        int[] Damage { get; }
        bool IsAreaScalingByLevel { get; }
        float[] Area { get; }
    }
}