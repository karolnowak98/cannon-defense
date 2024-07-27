using System.Collections.Generic;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemy
    {
        EnemyEntity Entity { get; }
        EnemyType Type { get; }
        float CurrentHealth { get; set; }
        float CurrentMoveSpeed { get; set; }
        void TakeDamage(int damage);
        void UpdateStatsByEffects(EnemyEffectTrigger trigger, IEnumerable<EnemyEffectEntity> effects);
        void ApplyEffects(float speedModifier, float healthModifier, bool shouldHealCompletely);
    }
}