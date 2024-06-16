using GlassyCode.CannonDefense.Game.Player.Data.Skills;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public interface ISkillEntityData
    {
        SkillName Name { get; }
        SkillUseType UseType { get; }
        InputAction InputAction { get; }
        bool IsCooldownScalingByLevel { get; }
        float[] Cooldown { get; }
    } 
}