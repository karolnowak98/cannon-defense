using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Player.Data.CannonBall;
using UnityEngine;
using UnityEngine.Pool;

namespace GlassyCode.CannonDefense.Game.Player.Logic.CannonBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonBall : GlassyMonoBehaviour
    {
        [SerializeField] private CannonBallData _data;

        private Rigidbody _rb;
        
        public ObjectPool<CannonBall> Pool { get; set; }
        
        private void Awake()
        {
            TryGetComponent(out _rb);
        }

        public void Reset(Vector3 position)
        {
            SetPosition(position);
            _rb.velocity = Vector3.forward * _data.Speed;
            Enable();
        }
        
        private void OnCollisionEnter(Collision col)
        {
            var go = col.gameObject;
            
            if (!go.CompareTag("Enemy")) return;
                
            go.TryGetComponent<Enemy>(out var enemyMb);

            enemyMb?.TakeDamage(2);
            
            Pool.Release(this);
        }
    }
}