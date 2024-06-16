using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.CannonBall
{
    [CreateAssetMenu(menuName = MenuNames.Entities + nameof(CannonBallData), fileName = nameof(CannonBallData))]
    public class CannonBallData : EntityData, ICannonBallData
    {
        [field: Tooltip("The speed of the projectile, in this case a cannon ball.")]
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        
        [field: Tooltip("Basic cannon ball damage.")]
        [field: SerializeField] public int Damage { get; private set; } = 10;
    }
}