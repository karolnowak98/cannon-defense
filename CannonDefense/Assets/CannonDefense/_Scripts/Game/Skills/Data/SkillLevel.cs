using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Skills.Data
{
    [Serializable]
    public struct SkillLevel
    {
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}