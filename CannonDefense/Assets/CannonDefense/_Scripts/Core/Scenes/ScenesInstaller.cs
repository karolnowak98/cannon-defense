using Zenject;

namespace GlassyCode.CannonDefense.Core.Scenes
{
    public class ScenesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScenesController>().To<ScenesController>().AsSingle().NonLazy();
        }
    }
}
