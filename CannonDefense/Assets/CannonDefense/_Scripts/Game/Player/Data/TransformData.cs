using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct TransformData
    {
        [field: SerializeField, Header("Transform")] public Vector3 StartingPosition { get; private set; }
    }
}