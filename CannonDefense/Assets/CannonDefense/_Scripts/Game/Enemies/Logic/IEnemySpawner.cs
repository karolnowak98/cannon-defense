namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public interface IEnemySpawner
    {
        void Tick();
        void StartSpawning();
        void StopSpawning();
    }
}