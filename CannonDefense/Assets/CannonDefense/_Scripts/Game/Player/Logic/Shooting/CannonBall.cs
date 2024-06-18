using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Game.Enemies.Logic;
using GlassyCode.CannonDefense.Game.Player.Data.CannonBall;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class CannonBall : GlassyObjectPoolElement<CannonBall>
    {
        [SerializeField] private ProjectileEntity _entity;
        [Inject] private IPlayerManager _playerManager;

        private Rigidbody _rb;
        
        private void Awake()
        {
            TryGetComponent(out _rb);
        }
        
        public override void Reset()
        {
            _rb.velocity = Vector3.forward * _entity.Speed;
            Enable();
        }

        private void OnTriggerEnter(Collider col)
        {
            var go = col.gameObject;

            if (go.CompareTag("Enemy"))
            {
                go.TryGetComponent<Enemy>(out var enemy);

                enemy?.TakeDamage(_playerManager.Stats.CurrentDamage);

                Pool.Release(this);
            }

            if (go.CompareTag("EnemySpawner"))
            {
                Pool.Release(this);
            }
        }
        
        public sealed class Factory : PlaceholderFactory<Object, CannonBall>{ }
    }
}