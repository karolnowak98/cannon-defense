using GlassyCode.CannonDefense.Game.Player.Enums;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills
{
    public interface ISkillEntity
    {
        SkillName Name { get; }
        InputAction InputAction { get; }
        bool DoesCooldownScaleWithLevel { get; }
        float[] Cooldown { get; }
        float CooldownUIRefreshInterval { get; }
    } 
}