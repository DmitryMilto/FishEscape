using UnityEngine;

namespace _Project.UI
{
    public abstract class UIPanelBase : MonoBehaviour, IPanel
    {
        [SerializeField] protected CanvasGroup canvasGroup;

        public virtual void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            gameObject.SetActive(false);
        }
    }
}