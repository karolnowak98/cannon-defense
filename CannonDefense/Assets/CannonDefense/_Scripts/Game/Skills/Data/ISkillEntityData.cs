using GlassyCode.CannonDefense.Game.Skills.Enums;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Skills.Data
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