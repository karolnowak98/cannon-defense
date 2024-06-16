using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Editors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills
{
    public abstract class SkillData : EntityData
    {
        [field: Header("Base Stats")]
        [field: SerializeField] public SkillName Name { get; private set; }
        
        [field: SerializeField] public SkillUseType UseType { get; private set; }
        
        [field: SerializeField] public InputAction InputAction { get; private set; }
        
        [field: SerializeField, ReadOnly] public bool IsCooldownScalingByLevel { get; private set; }
        
        [Tooltip("The cooldowns are sorted from first to the highest.")]
        [field: SerializeField] public float[] Cooldown { get; private set; }
        
        protected virtual void OnValidate()
        {
            if (Cooldown is null || Cooldown.Length <= 0)
                return;
            
            IsCooldownScalingByLevel = Cooldown.Length > 1;
        }
    }
}