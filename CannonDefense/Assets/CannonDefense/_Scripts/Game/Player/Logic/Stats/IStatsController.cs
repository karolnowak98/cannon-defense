namespace GlassyCode.CannonDefense.Game.Player.Logic.Stats
{
    public interface IStatsController
    {
        void Reset();
        void TakeDamage(int damage);
    }
}