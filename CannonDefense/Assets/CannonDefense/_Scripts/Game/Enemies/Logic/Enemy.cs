using GlassyCode.CannonDefense.Core.Pool;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class Enemy : GlassyObjectPoolElement<Enemy>, IEnemy
    {
        [SerializeField] private EnemyData _data;

        private Rigidbody _rb;
        private float _currentHealth;
        
        private void Awake()
        {
            TryGetComponent(out _rb);
            _currentHealth = _data.Health;
        }

        public override void Reset()
        {
            //SetRandomPosition
            //SetPosition();
            _rb.velocity = Vector3.forward * _data.MoveSpeed;
            Enable();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Pool.Release(this);
        }
    }
}