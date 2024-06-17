namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemiesManager
    {
        IEnemySpawner Spawner { get; }
    }
}