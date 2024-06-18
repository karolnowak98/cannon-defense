using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.CannonDefense.Core.UI
{
    public abstract class UISliderElement : UIElement
    {
        [SerializeField] protected Slider _slider;

        protected void UpdateSlider(float value)
        {
            _slider.value = value;
        }
    }
}