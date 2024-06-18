using System;
using GlassyCode.CannonDefense.Core.Editors;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Stats
{
    [Serializable]
    public class StatsData
    {
        [Tooltip("Max player level.")]
        [field: SerializeField, Range(1, 100)] public int MaxLevel { get; private set; }
        
        [Tooltip("Required experience is sorted from first to highest level. Experience is not reset when leveling up!!")]
        [field: SerializeField, Range(1, 9999)] public int[] RequiredExpToLevelUp { get; private set; }

        [field: SerializeField, ReadOnly] public bool DoesDamageScaleWithLevel { get; private set; }
        
        [Tooltip("Cooldown is sorted from first to highest level.")]
        [field: SerializeField, Range(1, 1000)] public int[] Damage { get; private set; }
        
        [Tooltip("Initial health.")]
        [field: SerializeField, Range(1, 9999)] public int Health { get; private set; }

        public void UpdateBooleans()
        {
            if (Damage is not null && Damage.Length > 0) DoesDamageScaleWithLevel = Damage.Length > 1;
        }
    }
}