using GlassyCode.CannonDefense.Game.Enemies.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesConfig _config;
        
        public override void InstallBindings()
        {
            Container.Bind<IEnemiesConfig>().To<EnemiesConfig>().FromInstance(_config).AsSingle();

            Container.Bind(typeof(EnemiesController), typeof(IEnemiesController)).To<EnemiesController>()
                .AsSingle()
                .WithArguments(_config);
        }
    }
}