using UnityEngine;

namespace GlassyCode.CannonDefense.Core.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIElement : MonoBehaviour
    {
        [SerializeField] protected bool _enableOnStart; 
        
        protected CanvasGroup CanvasGroup;

        protected bool IsVisible => CanvasGroup.alpha != 0;
        
        protected virtual void Awake()
        {
            TryGetComponent(out CanvasGroup);

            if (_enableOnStart)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        protected virtual void Show()
        {
            if (IsVisible) return;
            
            CanvasGroup.alpha = 1;
            CanvasGroup.interactable = true;
            CanvasGroup.blocksRaycasts = true;
        }

        protected virtual void Hide()
        {
            if (!IsVisible) return;
            
            CanvasGroup.alpha = 0;
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }

        protected virtual void ToggleVisibility()
        {
            if (IsVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}