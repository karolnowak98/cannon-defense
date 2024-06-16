using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Stats
{
    [Serializable]
    public struct PlayerLevel
    {
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public int RequiredExpForLevelUp { get; private set; }
    }
}