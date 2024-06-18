using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public abstract class Skill : ISkill
    {
        public bool CanUse { get; set; }
        public Transform Parent { get; set; }
        
        public virtual void Tick()
        {
            
        }

        public virtual void Enable()
        {
            CanUse = true;
        }

        public virtual void Disable()
        {
            CanUse = false;
        }

        public virtual void Reset()
        {
            
        }

        public void SetProjectilesParent(Transform parent)
        {
            Parent = parent;
        }
    }
}