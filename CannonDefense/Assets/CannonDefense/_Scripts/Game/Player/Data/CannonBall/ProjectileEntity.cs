using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Utility;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Data.CannonBall
{
    [CreateAssetMenu(menuName = MenuNames.Entities + nameof(ProjectileEntity), fileName = nameof(ProjectileEntity))]
    public class ProjectileEntity : EntityData
    {
        [field: Tooltip("Speed of projectile, in this case a cannon ball.")]
        [field: SerializeField] public float Speed { get; private set; }
        
        [field: Tooltip("Damage of projectile.")]
        [field: SerializeField] public int Damage { get; private set; }
    }
}