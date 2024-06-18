using GlassyCode.CannonDefense.Core.UI;
using GlassyCode.CannonDefense.Game.Player.Enums;
using GlassyCode.CannonDefense.Game.Player.Logic.Signals;
using GlassyCode.CannonDefense.Game.Player.Logic.Skills.Signals;
using UnityEngine;
using Zenject;

namespace GlassyCode.CannonDefense.Game.Player.UI
{
    public class CooldownSlider : UISliderElement
    {
        [SerializeField] private SkillName _skillName;

        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<SkillUsedSignal>(ShowSlider);
            _signalBus.Subscribe<SkillCooldownExpiredSignal>(HideSlider);
            _signalBus.Subscribe<PlayerResetSignal>(Hide);
            _signalBus.Subscribe<SkillCooldownUpdatedSignal>(UpdateSlider);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<SkillUsedSignal>(ShowSlider);
            _signalBus.TryUnsubscribe<SkillCooldownExpiredSignal>(HideSlider);
            _signalBus.TryUnsubscribe<PlayerResetSignal>(Hide);
            _signalBus.TryUnsubscribe<SkillCooldownUpdatedSignal>(UpdateSlider);
        }
        
        private void ShowSlider(SkillUsedSignal signal)
        {
            if (signal.Name != _skillName) return;
            
            Show();
        }

        private void HideSlider(SkillCooldownExpiredSignal signal)
        {
            if (signal.Name != _skillName) return;
            
            Hide();
        }

        private void UpdateSlider(SkillCooldownUpdatedSignal signal)
        {
            if (signal.Name != _skillName) return;
            
            UpdateSlider(signal.Cooldown);
        }
    }
}