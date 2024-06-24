using GlassyCode.CannonDefense.Game.Enemies.Data;
using GlassyCode.CannonDefense.Game.Enemies.Logic.Signals;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace GlassyCode.CannonDefense.Game.Enemies.Logic
{
    public sealed class EnemiesInstaller : MonoInstaller
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
            BindFactories();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<EnemyCrossedFinishLine>();
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<EnemyWoundedSignal>();
        }

        private void BindFactories()
        {
            Container.BindFactory<Object, Enemy, Enemy.Factory>().FromFactory<PrefabFactory<Enemy>>();
        }
    }
}