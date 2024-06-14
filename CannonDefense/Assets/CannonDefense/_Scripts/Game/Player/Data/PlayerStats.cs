using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct PlayerStats
    {
        [field: SerializeField] public int[] RequiredExperience { get; private set; }
        [field: SerializeField] public int[] Damage { get; private set; }
        [field: SerializeField] public float StartingMovementSpeed { get; private set; }
        [field: SerializeField] public int StartingHealth { get; private set; }
    }
}