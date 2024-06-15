using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct StatsData
    {
        [field: SerializeField] public PlayerLevel[] Levels { get; private set; }
        [field: SerializeField] public int Health { get; private set; }
    }
}