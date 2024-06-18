using GlassyCode.CannonDefense.Core.Data;
using GlassyCode.CannonDefense.Core.Editors;
using GlassyCode.CannonDefense.Game.Player.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GlassyCode.CannonDefense.Game.Player.Data.Skills
{
    public abstract class SkillEntity : EntityData
    {
        [field: Header("Base Stats")]
        [field: SerializeField] public SkillName Name { get; private set; }
        
        [field: SerializeField] public InputAction InputAction { get; private set; }
        [field: SerializeField, ReadOnly] public bool DoesCooldownScaleWithLevel { get; private set; }
        
        [Tooltip("The cooldowns are sorted from first to the highest.")]
        [field: SerializeField] public float[] Cooldown { get; private set; }
        
        [Tooltip("How often UI (e. g. slider) refresh when skill is on cooldown.")]
        [field: SerializeField] public float CooldownUIRefreshInterval { get; private set; }
        
        protected virtual void OnValidate()
        {
            if (Cooldown is { Length: > 0 })
            {
                DoesCooldownScaleWithLevel = Cooldown.Length > 1;
            }
        }
    }
}