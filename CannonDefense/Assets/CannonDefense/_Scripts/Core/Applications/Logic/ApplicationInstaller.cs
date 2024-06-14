using GlassyCode.CannonDefense.Core.Applications.Data;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GlassyCode.CannonDefense.Core.Applications.Logic
{
    public sealed class ApplicationInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_applicationConfig")] [SerializeField] private ApplicationConfigData _applicationConfigData;
        
        public override void InstallBindings()
        {
            Container.Bind<IApplicationConfig>().To<ApplicationConfigData>().FromInstance(_applicationConfigData).AsSingle();
            
            Container.Bind(typeof(ApplicationController), typeof(IApplicationController), typeof(IInitializable))
                .To<ApplicationController>().AsSingle().NonLazy();
        }
    }
}