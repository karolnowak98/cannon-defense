using Zenject;

namespace GlassyCode.CannonDefense.Core.Signals
{
    public sealed class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}