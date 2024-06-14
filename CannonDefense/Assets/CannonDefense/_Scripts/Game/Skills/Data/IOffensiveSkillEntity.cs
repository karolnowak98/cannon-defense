using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Skills.Data
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