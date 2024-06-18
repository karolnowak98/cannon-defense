using UnityEngine;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemy
    {
        Transform Transform { get; }
        void TakeDamage(int damage);
    }
}