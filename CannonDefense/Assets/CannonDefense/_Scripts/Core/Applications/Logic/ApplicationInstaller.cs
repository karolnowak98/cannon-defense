using GlassyCode.CannonDefense.Core.Applications.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Applications.Logic
{
    public sealed class ApplicationInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_applicationConfigData")] [SerializeField] private ApplicationConfig _applicationConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<IApplicationConfig>().To<ApplicationConfig>().FromInstance(_applicationConfig).AsSingle();
            
            Container.Bind(typeof(ApplicationController), typeof(IApplicationController), typeof(IInitializable))
                .To<ApplicationController>().AsSingle().NonLazy();
        }
    }
}