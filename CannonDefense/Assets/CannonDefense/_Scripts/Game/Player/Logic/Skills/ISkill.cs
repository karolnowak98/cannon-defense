using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills
{
    public interface ISkill
    {
        bool CanUse { get;}
        void Tick();
        void Enable();
        void Disable();
        void Reset();
        void SetProjectilesParent(Transform parent);
    }
}