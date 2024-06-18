using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Battlefield.Logic;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Battlefield.UI
{
    public sealed class EndUIElement : UIElement
    {
        [SerializeField] private Button _restartBtn; 
        [SerializeField] private TextMeshProUGUI _scoreTmp; 
        
        private IBattlefieldManager _battlefieldManager;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus, IBattlefieldManager battlefieldManager)
        {
            _signalBus = signalBus;
            _battlefieldManager = battlefieldManager;
            
            _signalBus.Subscribe<PlayerDiedSignal>(ShowPanel);
            _restartBtn.onClick.AddListener(RestartBattle);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<PlayerDiedSignal>(ShowPanel);
            _restartBtn.onClick.RemoveAllListeners();
        }

        private void ShowPanel(PlayerDiedSignal signal)
        {
            _scoreTmp.text = $"Score: {signal.Score}";
            Show();
        }

        private void RestartBattle()
        {
            _battlefieldManager.RestartBattle();
            Hide();
        }
    }
}