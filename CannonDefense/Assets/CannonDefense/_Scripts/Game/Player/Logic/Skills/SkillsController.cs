using System.Collections.Generic;
using GlassyCode.CannonDefense.Game.Player.Data.Skills.Offensive;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public sealed class SkillsController : ISkillsController
    {
        private readonly List<ISkill> _skills = new();
        private readonly SignalBus _signalBus;
        private readonly OffensiveSkillProjectile.Factory _factory;
        private readonly Transform _projectileSpawnPoint;
        private Transform _projectilesParent;

        public SkillsController(SignalBus signalBus, IEnumerable<OffensiveSkillEntity> offensiveSkills, OffensiveSkillProjectile.Factory factory, Transform projectileSpawnPoint)
        {
            _signalBus = signalBus;
            _factory = factory;
            _projectileSpawnPoint = projectileSpawnPoint;
            _projectilesParent = new GameObject(nameof(_projectilesParent)).transform;

            InitSkills(offensiveSkills);
        }
        
        public void Dispose()
        {
            Disable();
        }

        public void Tick()
        {
            foreach (var skill in _skills)
            {
                skill.Tick();
            }
        }

        public void Enable()
        {
            foreach (var skill in _skills)
            {
                skill.Enable();
            }
        }

        public void Disable()
        {
            foreach (var skill in _skills)
            {
                skill.Disable();
            }
        }

        public void Reset()
        {
            Object.Destroy(_projectilesParent.gameObject);
            _projectilesParent = new GameObject(nameof(_projectilesParent)).transform;
            
            foreach (var skill in _skills)
            {
                skill.Reset();
                skill.SetProjectilesParent(_projectilesParent);
            }
        }

        private void InitSkills(IEnumerable<OffensiveSkillEntity> offensiveSkills)
        {
            foreach (var skillEntity in offensiveSkills)
            {
                var skill = new OffensiveSkill(skillEntity, _signalBus, _factory, _projectileSpawnPoint, _projectilesParent);
                _skills.Add(skill);
            }
        }
    }
}