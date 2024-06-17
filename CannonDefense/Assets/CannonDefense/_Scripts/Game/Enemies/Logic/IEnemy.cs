using GlassyCode.CannonDefense.Game.Enemies.Data;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemy
    {
        EnemyType Type { get; }
        void TakeDamage(int damage);
    }
}