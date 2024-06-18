using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.UI
{
    public sealed class LevelUpTmp : UIFadeTmp
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<PlayerDiedSignal>(Hide);
            _signalBus.Subscribe<PlayerLeveledUpSignal>(UpdateLevelUpTmp);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerDiedSignal>(Hide);
            _signalBus.TryUnsubscribe<PlayerLeveledUpSignal>(UpdateLevelUpTmp);
        }

        private void UpdateLevelUpTmp(PlayerLeveledUpSignal signal)
        {
            SetText($"Level {signal.Level} Achieved!");
            Show();
        }
    }
}