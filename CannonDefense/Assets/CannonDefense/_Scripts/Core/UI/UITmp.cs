using TMPro;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.UI
{
    public abstract class UITmp : UIElement
    {
        [SerializeField] protected TextMeshProUGUI _tmp;

        public void SetText(string text) => _tmp.text = text;
        public void SetText(int number) => _tmp.text = $"{number}";
        public void SetText(float number) => _tmp.text = $"{number}";
    }
}