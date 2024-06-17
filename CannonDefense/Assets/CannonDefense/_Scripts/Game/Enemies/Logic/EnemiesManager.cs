using GlassyCode.CannonDefense.Core.Time;
using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemiesManager : IEnemiesManager, ITickable
    {
        public IEnemiesConfig Config { get; private set; } 
        public IEnemySpawner Spawner { get; private set; } 
        
        [Inject]
        private void Construct(ITimeController timeController, IEnemiesConfig config, Enemy.Factory factory, BoxCollider spawningArea)
        {
            Spawner = new EnemySpawner(timeController, config, factory, spawningArea);
        }

        public void Tick()
        {
            Spawner.Tick();
        }
    }
}