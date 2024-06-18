using GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive;
using GlassyCode.CannonDefense.Game.Player.Enums;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public class OffensiveSkill : Skill
    {
        private readonly SignalBus _signalBus;
        private readonly OffensiveSkillProjectile.Factory _factory;
        private readonly Transform _spawnPoint;
        private readonly Transform _parent;
        private readonly InputAction _input;
        private readonly SkillName _name;

        private OffensiveSkillProjectile _projectile;
        private bool _isProjectileFired;
        private bool _isCooldownActive;
        private float _cooldownTimer;
        private float _cooldownUIRefreshTimer;

        public IOffensiveSkillEntity Entity { get; }
        public float Cooldown { get; private set; }

        public OffensiveSkill(IOffensiveSkillEntity entity, SignalBus signalBus, OffensiveSkillProjectile.Factory factory, Transform spawnPoint, Transform parent)
        {
            Entity = entity;
            _parent = parent;
            _name = entity.Name;
            _signalBus = signalBus;
            _factory = factory;
            _spawnPoint = spawnPoint;
            _input = entity.InputAction;
            
            Cooldown = Entity.Cooldown[0];
            _cooldownTimer = 0;
            _cooldownUIRefreshTimer = 0;
            _isProjectileFired = false;
            _isCooldownActive = false;
        }

        public override void Tick()
        {
            if (!_isCooldownActive) return;
            
            _cooldownTimer -= Time.deltaTime;
                
            if (_cooldownTimer <= 0)
            {
                _cooldownTimer = 0;
                _isCooldownActive = false;
                _signalBus.TryFire(new SkillCooldownExpiredSignal { Name = _name });
            }

            _cooldownUIRefreshTimer -= Time.deltaTime;

            if (_cooldownUIRefreshTimer <= 0)
            {
                _cooldownUIRefreshTimer = Entity.CooldownUIRefreshInterval;
                var normalizedCooldown = _cooldownTimer / Cooldown;
                _signalBus.TryFire(new SkillCooldownUpdatedSignal { Cooldown = normalizedCooldown, Name = _name });
            }
        }

        public override void Enable()
        {
            _input.performed += ShootOrBoomProjectile;
            _input.Enable();
            base.Enable();
        }

        public override void Disable()
        {
            _input.performed -= ShootOrBoomProjectile;
            _input.Disable();
            base.Disable();
        }

        public override void Reset()
        {
            if (_projectile != null)
            {
                _projectile.DestroyWithoutTriggers();
            }
            
            Cooldown = Entity.Cooldown[0];
            _cooldownTimer = 0;
            _isProjectileFired = false;
            _isCooldownActive = false;
        }

        private void ShootOrBoomProjectile(InputAction.CallbackContext context)
        {
            if (CanUse && !_isCooldownActive)
            {
                if (!_isProjectileFired)
                {
                    ShootProjectile();
                }
                else
                {
                    BoomProjectile();
                }
            }
        }

        private void ShootProjectile()
        {
            _projectile = _factory.Create(Entity.OffensiveSkillProjectile);
            _projectile.SetPosition(_spawnPoint.position);
            _projectile.SetParent(_parent);
            _isProjectileFired = true;
        }

        private void BoomProjectile()
        {
            _projectile.Destroy();
            _cooldownTimer = Cooldown;
            _isCooldownActive = true;
            _isProjectileFired = false;
            _signalBus.TryFire(new SkillUsedSignal { Name = _name });
        }
    }
}