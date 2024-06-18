using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Player.Data.CannonBall;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    [RequireComponent(typeof(Rigidbody))]
    public class OffensiveSkillProjectile : GlassyMonoBehaviour
    {
        [SerializeField] private ProjectileEntity _entity;
        [SerializeField] private SphereCollider _damageArea;
        [Inject] private SignalBus _signalBus;
        
        private float _damageAreaRadius;
        private int _damage;
        private Rigidbody _rb;

        private void Awake()
        {
            TryGetComponent(out _rb);

            _rb.velocity = Vector3.forward * _entity.Speed;
            _damageAreaRadius = _damageArea.radius;
            _damage = _entity.Damage;
        }

        public override void Destroy()
        {
            _signalBus.TryFire(new SkillProjectileBoomedSignal
            {
                Radius = _damageAreaRadius,
                ExplosionCenter = transform.position,
                Damage = _damage
            });
            
            base.Destroy();
        }

        public void DestroyWithoutTriggers()
        {
            base.Destroy();
        }
        
        private void OnTriggerEnter(Collider col)
        {
            var go = col.gameObject;

            if (go.CompareTag("EnemySpawner"))
            {
                Destroy();
            }
        }
        
        public class Factory : PlaceholderFactory<Object, OffensiveSkillProjectile>{ }
    }
}