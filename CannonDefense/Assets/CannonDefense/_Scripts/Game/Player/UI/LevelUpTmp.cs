using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Player.Logic;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.UI
{
    public class LevelUpTmp : UIFadeTmp
    {
        private IPlayerManager _playerManager;

        [Inject]
        private void Construct(IPlayerManager playersManager)
        {
            _playerManager = playersManager;

            _playerManager.Stats.OnLevelUp += UpdateLevelUpTmp;
        }

        private void OnDestroy()
        {
            _playerManager.Stats.OnLevelUp -= UpdateLevelUpTmp;
        }

        private void UpdateLevelUpTmp(int level)
        {
            SetText($"Level {level} Achieved!");
            Show();
        }
    }
}