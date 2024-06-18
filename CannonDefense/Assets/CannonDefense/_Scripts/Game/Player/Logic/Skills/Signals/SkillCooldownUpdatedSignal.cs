using GlassyCode.CannonDefense.Game.Player.Enums;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals
{
    public struct SkillCooldownUpdatedSignal
    {
        public SkillName Name;
        public float Cooldown;
    }
}