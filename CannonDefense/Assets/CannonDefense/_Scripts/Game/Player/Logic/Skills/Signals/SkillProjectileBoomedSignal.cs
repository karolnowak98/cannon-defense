using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals
{
    public struct SkillProjectileBoomedSignal
    {
        public Vector3 ExplosionCenter { get; set; }
        public float Radius;
        public int Damage { get; set; }
    }
}