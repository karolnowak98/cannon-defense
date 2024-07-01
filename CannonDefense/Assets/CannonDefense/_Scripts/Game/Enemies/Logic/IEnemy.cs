using System.Collections.Generic;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Enums;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemy
    {
        void TakeDamage(int damage);
        void UpdateStatsByEffects(EnemyEffectTrigger trigger, IEnumerable<EnemyEffectEntity> effects);
    }
}