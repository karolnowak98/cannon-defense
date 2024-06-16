using Zenject;

namespace GlassyCode.CannonDefense.Core.Signals
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}