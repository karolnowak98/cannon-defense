using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.UI
{
    public sealed class ScoreTmp : UITmp
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<PlayerScoreUpdatedSignal>(SetScore);
            _signalBus.Subscribe<PlayerStatsResetSignal>(ResetScore);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerScoreUpdatedSignal>(SetScore);
            _signalBus.TryUnsubscribe<PlayerStatsResetSignal>(ResetScore);
        }

        private void ResetScore(PlayerStatsResetSignal signal)
        {
            SetText(signal.Stats.Score);
        }

        private void SetScore(PlayerScoreUpdatedSignal signal)
        {
            SetText(signal.Score);
        }
    }
}