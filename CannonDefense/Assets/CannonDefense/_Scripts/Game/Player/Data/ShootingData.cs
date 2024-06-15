using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data
{
    [Serializable]
    public struct ShootingData
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
        [field: SerializeField] public Vector3 ProjectileOffset { get; private set; }
    }
}