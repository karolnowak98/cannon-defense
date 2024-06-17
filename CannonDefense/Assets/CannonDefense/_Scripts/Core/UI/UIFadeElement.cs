using System.Collections;
using UnityEngine;

namespace GlassyCode.CannonDefense.Core.UI
{
    public abstract class UIFadeElement : UIElement
    {
        [SerializeField] private float _displayDuration = 1.0f; 

        private Coroutine _displayCoroutine;

        protected override void Show()
        {
            base.Show();
            
            if (_displayCoroutine != null)
            {
                StopCoroutine(_displayCoroutine);
            }
            
            _displayCoroutine = StartCoroutine(DisplayForDuration());
        }

        private IEnumerator DisplayForDuration()
        {
            yield return new WaitForSeconds(_displayDuration);
            Hide();
        }
    }
}