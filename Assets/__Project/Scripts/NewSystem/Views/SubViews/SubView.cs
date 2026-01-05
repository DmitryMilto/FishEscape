using System.Threading;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Interfaces.Animations;
using __Project.Scripts.NewSystem.Views.Animations;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Views.SubViews
{
    public class SubView : MonoBehaviour, ISubViewAnimator
    {
        private RectTransform _rect;
        private CanvasGroup _canvasGroup;
        private Vector2 _anchored;
        [SerializeField] private TypeAnimation _direction;

        protected virtual void Awake()
        {
            _rect ??= this.transform as RectTransform;
            _canvasGroup = GetComponent<CanvasGroup>();
            _anchored = _rect.anchoredPosition;
        }
        
        public async UniTask PlayOpenAsync(CancellationToken cancellationToken)
        {
            await ViewAnimator.AnimateRectAsync(_rect,_anchored, _direction, toCenter: true, isView: false, cancellationToken);
        }

        public async UniTask PlayCloseAsync(CancellationToken cancellationToken)
        {
            await ViewAnimator.AnimateRectAsync(_rect, _anchored, _direction, toCenter: false, isView: false, cancellationToken);
        }

        public void PlayOpen()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
            _rect.anchoredPosition = Vector2.zero; // Reset position to center
        }

        public void PlayClose()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
        }
    }
}