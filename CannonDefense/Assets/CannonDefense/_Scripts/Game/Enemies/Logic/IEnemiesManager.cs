namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemiesManager
    {
        IEnemyGrid Grid { get; }
        IEnemySpawner Spawner { get; }
    }
}