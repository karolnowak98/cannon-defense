using System;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.Shooting
{
    [Serializable]
    public struct ShootingData
    {
        [field: Tooltip("Basic cannonball damage.")]
        [field: SerializeField] public int Damage { get; private set; }
        
        [field: Tooltip("Prefab of CannonBall with the related component.")]
        [field: SerializeField] public Logic.CannonBall.CannonBall CannonBall { get; private set; }
        
        [field: Header("Cannon Ball Pool"), Tooltip("Default (initial) cannon ball pool size.")]
        [field: SerializeField] public int CannonBallPoolInitialSize { get; private set; }
        
        [field: Tooltip("Max cannon ball pool size. Make sure that pool won't reach that value.")]
        [field: SerializeField] public int CannonBallPoolMaxSize { get; private set; }
    }
}