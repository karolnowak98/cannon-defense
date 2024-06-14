using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Game.Skills.Enums;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Skills.Data
{
    public abstract class Skill : Entity
    {
        [field: SerializeField] public SkillType Type { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: SerializeField] public bool DoubleClickToActivate { get; private set; }
    }
}