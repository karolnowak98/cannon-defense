using GlassyCode.CannonDefense.Game.Player.Logic.Skills;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills
{
    public interface IOffensiveSkillEntity : ISkillEntityData
    {
        GameObject ProjectilePrefab { get; }
        float ProjectileSpeed { get; }
        bool IsDamageScalingByLevel { get; }
        int[] Damage { get; }
        bool IsAreaScalingByLevel { get; }
        float[] Area { get; }
    }
}