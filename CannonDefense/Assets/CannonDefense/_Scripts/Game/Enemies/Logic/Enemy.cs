using System.Collections.Generic;
using System.Linq;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Enemy : GlassyObjectPoolElement<Enemy>, IEnemy, IQuadtreeElement
    {
        [SerializeField] private EnemyEntity _entity;

        private SignalBus _signalBus;
        private IQuadtree _quadtree;
        private Rigidbody _rb;
        private MeshRenderer _meshRenderer;
        private float _currentHealth;
        private float _currentMoveSpeed;
        private Bounds _bounds;
        
        public EnemyType Type => _entity.Type;
        public Vector2 Position => new(transform.position.x, transform.position.z);
        public Rect Rect => _bounds.GetXZRect();

        [Inject]
        private void Construct(SignalBus signalBus, IQuadtree quadtree)
        {
            _signalBus = signalBus;
            _quadtree = quadtree;
        }
        
        private void Awake()
        {
            TryGetComponent(out _rb);
            TryGetComponent(out _meshRenderer);
            
            _bounds = _meshRenderer.bounds;
        }

        private void OnDisable()
        {
            _quadtree.RemoveElement(this);
        }

        private void FixedUpdate()
        {
            if (IsActive)
            {
                _quadtree.UpdateElementNode(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Finish")) return;
            
            _signalBus.Fire(new EnemyCrossedFinishLineSignal{Effects = _entity.Effects, Damage = _entity.Damage});
            
            if (IsActive)
            {
                Pool.Release(this);
            }
        }
        
        public override void Reset()
        {
            _currentHealth = _entity.MaxHealth;
            _currentMoveSpeed = _entity.MoveSpeed;
            _rb.velocity = Vector3.back * _currentMoveSpeed;
            _meshRenderer.material.color = Colors.GetRandomColor();
            _quadtree.AddElement(this);
            Enable();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _signalBus.Fire(new EnemyWoundedSignal{Effects = _entity.Effects});
            
            if (_currentHealth <= 0)
            {
                _signalBus.Fire(new EnemyDiedSignal{Effects = _entity.Effects, Score = _entity.Score, Experience = _entity.Experience});
                if (IsActive)
                {
                    Pool.Release(this);
                }
            }
        }
        
        public void UpdateStatsByEffects(EnemyEffectTrigger trigger, IEnumerable<EnemyEffectEntity> effects)
        {
            foreach (var effect in effects)
            {
                if (effect.EffectTrigger != trigger || !effect.AffectOthers || effect.AffectedEnemyTypes.All(enemyType => enemyType != Type)) continue;

                switch (effect.EffectType)
                {
                    case EnemyEffectType.AddMovementSpeedValue:
                        _currentMoveSpeed += effect.EffectValue;
                        break;
                    case EnemyEffectType.AddMovementSpeedPercentage:
                        _currentMoveSpeed += _currentMoveSpeed * effect.EffectValue;
                        break;
                    case EnemyEffectType.DecreaseMovementSpeedValue:
                        _currentMoveSpeed -= effect.EffectValue;
                        break;
                    case EnemyEffectType.DecreaseMovementSpeedPercentage:
                        _currentMoveSpeed -= _currentMoveSpeed * effect.EffectValue;
                        break;
                    case EnemyEffectType.HealValue:
                        _currentHealth += effect.EffectValue;
                        break;
                    case EnemyEffectType.HealPercentage:
                        _currentHealth += _currentHealth * effect.EffectValue;
                        break;
                    case EnemyEffectType.HealCompletelyIfLessPercentage:
                        if (HasLessHealthThanPercent(0.5f))
                        {
                            _currentHealth = _entity.MaxHealth;
                        }
                        break;
                }
            }

            _rb.velocity = Vector3.back * _currentMoveSpeed;
        }
        
        private bool HasLessHealthThanPercent(float percentage) => _currentHealth < _entity.MaxHealth * percentage;
        
        public sealed class Factory : PlaceholderFactory<Object, Enemy>{ }
    }
}