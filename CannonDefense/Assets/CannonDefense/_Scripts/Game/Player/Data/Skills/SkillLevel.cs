using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills
{
    [Serializable]
    public struct SkillLevel
    {
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}