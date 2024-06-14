using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct PlayerStats
    {
        [field: SerializeField] public PlayerLevel[] Levels { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}