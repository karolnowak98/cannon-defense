using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Player.Logic;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.UI
{
    public class ScoreTmp : UITmp
    {
        private IPlayerManager _playerManager;

        [Inject]
        private void Construct(IPlayerManager playersManager)
        {
            _playerManager = playersManager;

            _playerManager.Stats.OnScoreUpdated += SetText;
        }

        private void OnDestroy()
        {
            _playerManager.Stats.OnScoreUpdated -= SetText;
        }
    }
}