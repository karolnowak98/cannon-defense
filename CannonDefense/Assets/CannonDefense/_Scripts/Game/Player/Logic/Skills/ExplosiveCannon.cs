using GlassyCode.CannonDefense.Game.Player.Data.Skills;
using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public class ExplosiveCannon : Skill, ITwoStepSkill
    {
        public bool CanUse { get; private set; }
        public bool IsCannonInAir;
        private GameObject _projectile; 

        public ExplosiveCannon(IOffensiveSkillEntity skillEntity) : base(skillEntity)
        {
            skillEntity.InputAction.performed += _ => Prepare();
        }
        
        public void Prepare()
        {
            CanUse = true;

            //_projectile = Object.Instantiate();
        }

        public override void Use()
        {
            
        }
    }
}