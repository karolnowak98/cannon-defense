using Zenject;

namespace GlassyCode.CannonDefense.Core.Time
{
    public sealed class TimeControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITimeController>().To<TimeController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
