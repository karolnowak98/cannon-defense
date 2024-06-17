using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public class EnemiesInstaller : MonoInstaller
    {
        [SerializeField] private EnemiesConfig _config;
        [SerializeField] private BoxCollider _spawningArea;
        
        public override void InstallBindings()
        {
            Container.Bind<IEnemiesConfig>().To<EnemiesConfig>().FromInstance(_config).AsSingle();

            Container.Bind(typeof(EnemiesManager), typeof(IEnemiesManager), typeof(ITickable))
                .To<EnemiesManager>()
                .AsSingle()
                .WithArguments(_spawningArea);
            
            DeclareSignals();
            BindFactory();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<EnemyAttackedSignal>();
            Container.DeclareSignal<EnemyKilledSignal>();
        }

        private void BindFactory()
        {
            Container.BindFactory<Object, Enemy, Enemy.Factory>().FromFactory<PrefabFactory<Enemy>>();
        }
    }
}