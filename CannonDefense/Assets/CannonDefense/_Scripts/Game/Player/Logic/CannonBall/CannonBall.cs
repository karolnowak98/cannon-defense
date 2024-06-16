using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Player.Data.CannonBall;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.CannonBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonBall : GlassyObjectPoolElement<CannonBall>
    {
        [SerializeField] private CannonBallData _data;

        private Rigidbody _rb;
        
        private void Awake()
        {
            TryGetComponent(out _rb);
        }
        
        public override void Reset()
        {
            _rb.velocity = Vector3.forward * _data.Speed;
            Enable();
        }

        private void OnTriggerEnter(Collider col)
        {
            var go = col.gameObject;
            
            if (!go.CompareTag("Enemy")) return;

            go.TryGetComponent<Enemy>(out var enemyMb);

            enemyMb?.TakeDamage(2);
            
            if (IsActive)
            {
                Pool?.Release(this);
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            
        }
    }
}