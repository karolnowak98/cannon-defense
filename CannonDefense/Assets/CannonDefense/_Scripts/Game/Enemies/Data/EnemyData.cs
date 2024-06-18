using System;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Data
{
    [Serializable]
    public struct EnemyData
    {
        [field: SerializeField] public Enemy Enemy { get; private set; }
        
        [field: Tooltip("Percentage value.")]
        [field: SerializeField, Range(0, 100)] public float SpawnRatio { get; private set; }
    }
}