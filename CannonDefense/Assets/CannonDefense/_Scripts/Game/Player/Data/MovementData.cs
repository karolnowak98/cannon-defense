using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct MovementData
    {
        [field: Header("Movement Speed"), Tooltip("The base movement speed of the player.")]
        [field: SerializeField] public float MoveSpeed { get; private set; }

        [field: Header("Max Movement Speed"), Tooltip("The maximum movement speed of the player.")]
        [field: SerializeField] public float MaxMoveSpeed { get; private set; }

        [field: Header("Other Properties"), Tooltip("The drag force applied to the player.")]
        [field: SerializeField] public float DragForce { get; private set; }
    }
}