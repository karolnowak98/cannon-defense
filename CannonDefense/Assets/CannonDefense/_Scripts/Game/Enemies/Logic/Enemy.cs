using System.Collections.Generic;
using System.Linq;
using GlassyCode.CannonDefense.Core.Grid.QuadTree.Logic;
using GlassyCode.CannonDefense.Core.Pools.Object;
using GlassyCode.CannonDefense.Core.Utility;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using Unity.Collections;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Enemy : GlassyObjectPoolElement<Enemy>, IEnemy, IQuadtreeElement
    {
        [field: SerializeField] public EnemyEntity Entity { get; private set; }

        private SignalBus _signalBus;
        private IQuadtree _quadtree;
        private Rigidbody _rb;
        private MeshRenderer _meshRenderer;
        private float _currentHealth;
        private float _currentMoveSpeed;
        private Bounds _bounds;
        
        public EnemyType Type => Entity.Type;
        public float CurrentHealth { get; set; }
        public float CurrentMoveSpeed { get; set; }
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
            
            _signalBus.Fire(new EnemyCrossedFinishLineSignal{Effects = Entity.Effects, Damage = Entity.Damage});
            
            if (IsActive)
            {
                Pool.Release(this);
            }
        }
        
        public override void Reset()
        {
            _currentHealth = Entity.MaxHealth;
            _currentMoveSpeed = Entity.MoveSpeed;
            _rb.velocity = Vector3.back * _currentMoveSpeed;
            _meshRenderer.material.color = Colors.GetRandomColor();
            _quadtree.AddElement(this);
            Enable();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _signalBus.Fire(new EnemyWoundedSignal{Effects = Entity.Effects});
            
            if (_currentHealth <= 0)
            {
                _signalBus.Fire(new EnemyDiedSignal{Effects = Entity.Effects, Score = Entity.Score, Experience = Entity.Experience});
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
                        /*if (HasLessHealthThanPercent(0.5f))
                        {
                            _currentHealth = Entity.MaxHealth;
                        }*/
                        break;
                }
            }

            _rb.velocity = Vector3.back * _currentMoveSpeed;
        }

        public struct Data
        {
            public float MoveSpeed;
            public float Health;
            public float MaxHealth;
            public EnemyType Type;
            
            public Data(IEnemy enemy)
            {
                MoveSpeed = enemy.CurrentMoveSpeed;
                Health = enemy.CurrentHealth;
                MaxHealth = enemy.Entity.MaxHealth;
                Type = EnemyType.BigCube;
            }

            public void ApplyEffects(EnemyEffectTrigger trigger, NativeArray<EnemyEffectEntityData> effects)
            {
                foreach (var effect in effects)
                {
                    var type = Type;
                    
                    if (effect.EffectTrigger != trigger || !effect.AffectOthers || effect.AffectedEnemyTypes.All(enemyType => enemyType != type)) continue;

                    switch (effect.EffectType)
                    {
                        case EnemyEffectType.AddMovementSpeedValue:
                            MoveSpeed += effect.EffectValue;
                            break;
                        case EnemyEffectType.AddMovementSpeedPercentage:
                            MoveSpeed += MoveSpeed * effect.EffectValue;
                            break;
                        case EnemyEffectType.DecreaseMovementSpeedValue:
                            MoveSpeed -= effect.EffectValue;
                            break;
                        case EnemyEffectType.DecreaseMovementSpeedPercentage:
                            MoveSpeed -= MoveSpeed * effect.EffectValue;
                            break;
                        case EnemyEffectType.HealValue:
                            Health += effect.EffectValue;
                            break;
                        case EnemyEffectType.HealPercentage:
                            Health += Health * effect.EffectValue;
                            break;
                        case EnemyEffectType.HealCompletelyIfLessPercentage:
                            if (HasLessHealthThanPercent(0.5f))
                            {
                                Health = MaxHealth;
                            }
                            break;
                    }
                }
            }
            
            private bool HasLessHealthThanPercent(float percentage) => Health < MaxHealth * percentage;
        }

        public void ApplyEffects(float speedModifier, float healthModifier, bool shouldHealCompletely)
        {
            //
        }
        
        public sealed class Factory : PlaceholderFactory<Object, Enemy>{ }
    }
}