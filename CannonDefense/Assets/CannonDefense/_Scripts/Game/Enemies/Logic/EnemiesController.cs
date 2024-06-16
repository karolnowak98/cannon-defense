using GlassyCode.CannonDefense.Game.Enemies.Data;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemiesController : IEnemiesController
    {
        public IEnemiesConfig Config { get; private set; } 
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(IEnemiesConfig config)
        {
            Config = config;

            Spawner = new EnemySpawner();
        }
    }
}