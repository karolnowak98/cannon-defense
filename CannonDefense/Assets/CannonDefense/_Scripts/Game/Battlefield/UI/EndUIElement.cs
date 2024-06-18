using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Battlefield.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.UI
{
    public sealed class EndUIElement : UIElement
    {
        [SerializeField] private Button _restartBtn; 
        
        private IBattlefieldManager _battlefieldManager;
        
        [Inject]
        private void Construct(IBattlefieldManager battlefieldManager)
        {
            _battlefieldManager = battlefieldManager;
            
            _restartBtn.onClick.AddListener(RestartBattle);
            _battlefieldManager.OnEndBattle += Show;
        }

        private void OnDestroy()
        {
            _restartBtn.onClick.RemoveAllListeners();
            _battlefieldManager.OnEndBattle -= Show;
        }

        private void RestartBattle()
        {
            _battlefieldManager.RestartBattle();
            Hide();
        }
    }
}