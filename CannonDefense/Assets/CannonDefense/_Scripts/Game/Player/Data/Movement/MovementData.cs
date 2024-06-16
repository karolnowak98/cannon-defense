using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Movement
{
    [Serializable]
    public struct MovementData
    {
        [field: Header("Position"), Tooltip("Initial position of the player.")]
        [field: SerializeField] public Vector3 InitialPosition { get; private set; }
        
        [field: Header("Movement MoveSpeed"), Tooltip("The base movement speed of the player.")]
        [field: SerializeField] public float MoveSpeed { get; private set; }

        [field: Tooltip("The maximum movement speed of the player.")]
        [field: SerializeField] public float MaxMoveSpeed { get; private set; }

        [field: Header("Other Properties"), Tooltip("The drag force applied to the player.")]
        [field: SerializeField] public float DragForce { get; private set; }
    }
}