using System;
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
    public class Enemy : GlassyObjectPoolElement<Enemy>, IEnemy, ISpatialObject
    {
        [SerializeField] private EnemyEntity _entity;

        private SignalBus _signalBus;
        private IQuadtree _quadtree;
        private Rigidbody _rb;
        private MeshRenderer _meshRenderer;
        private float _currentHealth;
        private float _currentMoveSpeed;
        
        public EnemyType Type => _entity.Type;
        public Vector3 Position => Transform.position;
        public Rect Rect => _meshRenderer.bounds.GetRect();

        [Inject]
        private void Construct(SignalBus signalBus, IQuadtree quadtree)
        {
            _signalBus = signalBus;
            _quadtree = quadtree;
            
            _signalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
            _signalBus.Subscribe<EnemyWoundedSignal>(OnEnemyWounded);
            _signalBus.Subscribe<EnemyCrossedFinishLine>(OnEnemyCrossedFinishLine);
        }
        
        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<EnemyDiedSignal>(OnEnemyDied);
            _signalBus.TryUnsubscribe<EnemyWoundedSignal>(OnEnemyWounded);
            _signalBus.TryUnsubscribe<EnemyCrossedFinishLine>(OnEnemyCrossedFinishLine);
        }
        
        private void Awake()
        {
            TryGetComponent(out _rb);
            TryGetComponent(out _meshRenderer);
            
            _meshRenderer.material.color = Colors.GetRandomColor();
            _quadtree.AddObject(this);
        }

        private void FixedUpdate()
        {
            _quadtree.UpdateObjectPosition(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Finish")) return;
            
            _signalBus.TryFire(new EnemyCrossedFinishLine { Effects = _entity.Effects, Damage = _entity.Damage });
            
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
            Enable();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _signalBus.TryFire(new EnemyWoundedSignal { Effects = _entity.Effects});
            
            if (_currentHealth <= 0)
            {
                _signalBus.TryFire(new EnemyDiedSignal { Effects = _entity.Effects, Score = _entity.Score, Experience = _entity.Experience});
                if (IsActive)
                {
                    Pool.Release(this);
                }
            }
        }
        
        private void UpdateStatsByEffects(EnemyEffectTrigger trigger, IEnumerable<EnemyEffectEntity> effects)
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
        private void OnEnemyDied(EnemyDiedSignal signal) => UpdateStatsByEffects(EnemyEffectTrigger.Died, signal.Effects);
        private void OnEnemyWounded(EnemyWoundedSignal signal) => UpdateStatsByEffects(EnemyEffectTrigger.Wounded, signal.Effects);
        private void OnEnemyCrossedFinishLine(EnemyCrossedFinishLine signal) => UpdateStatsByEffects(EnemyEffectTrigger.CrossedFinishLine, signal.Effects);
        
        public sealed class Factory : PlaceholderFactory<Object, Enemy>{ }
    }
}