using GlassyCode.CannonDefense.Core.Applications.Data;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Applications
{
    public sealed class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationConfig _applicationConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IApplicationConfig>().To<ApplicationConfig>().FromInstance(_applicationConfig).AsSingle();
            
            Container.Bind(typeof(ApplicationController), typeof(IApplicationController), typeof(IInitializable))
                .To<ApplicationController>().AsSingle().NonLazy();
        }
    }
}