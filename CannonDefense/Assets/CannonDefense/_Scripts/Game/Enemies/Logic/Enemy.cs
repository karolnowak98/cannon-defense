using GlassyCode.CannonDefense.Core.Pools;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class Enemy : GlassyObjectPoolElement<Enemy>, IEnemy
    {
        [SerializeField] private EnemyData _data;

        private SignalBus _signalBus;
        private Rigidbody _rb;
        private float _currentHealth;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            TryGetComponent(out _rb);
            _currentHealth = _data.Health;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                CrossFinishLine();
            }
        }

        public override void Reset()
        {
            _rb.velocity = Vector3.back * _data.MoveSpeed;
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
            _signalBus.TryFire(new EnemyKilledSignal { Type = _data.Type, Score = _data.Score, Experience = _data.Experience});
            Pool?.Release(this);
        }

        private void CrossFinishLine()
        {
            _signalBus.TryFire(new EnemyCrossFinishLineSignal { Damage = _data.Damage });
            Pool?.Release(this);
        }
        
        public class Factory : PlaceholderFactory<Object, Enemy>{ }
    }
}