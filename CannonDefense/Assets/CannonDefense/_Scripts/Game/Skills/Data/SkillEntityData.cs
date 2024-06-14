using System.Linq;
using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Editors;
using GlassyCode.CannonDefense.Game.Skills.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Skills.Data
{
    public abstract class SkillEntityData : EntityData
    {
        [field: SerializeField, Header("Base Stats")] public SkillName Name { get; private set; }
        [field: SerializeField] public SkillUseType UseType { get; private set; }
        [field: SerializeField] public InputAction InputAction { get; private set; }
        [field: SerializeField, ReadOnly] public bool IsCooldownScalingByLevel { get; private set; }
        [field: SerializeField, Tooltip("The cooldowns are sorted from first to the highest.")] public float[] Cooldown { get; private set; }
        
        protected virtual void OnValidate()
        {
            SetIsCooldownScaling();
            ValidateBindings();
        }

        private void SetIsCooldownScaling()
        {
            if (Cooldown is null || Cooldown.Length <= 0)
                return;
            
            IsCooldownScalingByLevel = Cooldown.Length > 1;
        }

        private void ValidateBindings()
        {
            //TODO find asset and check there
            if (InputAction == null)
                return;
            
            foreach (var binding in InputAction.bindings.Where(binding => binding.effectivePath is "<Keyboard>/a" or "<Keyboard>/d" or "<Keyboard>/space"))
            {
                Debug.LogWarning($"InputAction '{InputAction.name}' has an undesirable key assigned: {binding.path}");
            }
        }
    }
}