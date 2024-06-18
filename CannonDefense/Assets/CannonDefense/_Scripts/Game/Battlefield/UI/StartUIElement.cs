using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Battlefield.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.UI
{
    public sealed class StartUIElement : UIElement
    {
        [SerializeField] private Button _startBtn;

        private IBattlefieldManager _battlefieldManager;

        [Inject]
        private void Construct(IBattlefieldManager battlefieldManager)
        {
            _battlefieldManager = battlefieldManager;
            
            _startBtn.onClick.AddListener(StartBattle);
        }

        private void OnDestroy()
        {
            _startBtn.onClick.RemoveAllListeners();
        }

        private void StartBattle()
        {
            _battlefieldManager.StartBattle();
            Hide();
        }
    }
}