using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility.Static;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.CannonBall
{
    [CreateAssetMenu(menuName = MenuNames.Entities + nameof(CannonBallData), fileName = nameof(CannonBallData))]
    public class CannonBallData : EntityData, ICannonBallData
    {
        [field: Tooltip("The speed of the projectile, in this case a cannonball.")]
        [field: SerializeField] public float Speed { get; private set; } = 10f;
    }
}