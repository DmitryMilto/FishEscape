using System.Collections.Generic;
using System.Linq;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Views.Animations;
using __Project.Scripts.NewSystem.Views.Managers;
using __Project.Scripts.NewSystem.Views.SubViews;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Base
{
    public abstract class AViewBase : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string LogPrefix => $"<color=#A6F6E6>[{GetType().Name}]</color>";
#else
        protected string LogPrefix => $"[{GetType().Name}]";
#endif
        [Inject] protected ViewManager Manager;

        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] private List<SubView> _subViews;
        [SerializeField] private TypeAnimation _animation = TypeAnimation.None;
        [SerializeField] protected ETypeView _viewType;

        private RectTransform _rectTransform;
        private Vector2 _defaultAnchoredPosition;

        public ETypeView ViewType => _viewType;

        protected virtual void Awake()
        {
            _rectTransform = transform as RectTransform;
            _canvasGroup ??= GetComponent<CanvasGroup>();
            if (_rectTransform != null)
                _defaultAnchoredPosition = _rectTransform.anchoredPosition;
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            _canvas ??= GetComponent<Canvas>();
            _canvasGroup ??= GetComponent<CanvasGroup>();
        }
#endif

        /// <summary>
        /// Асинхронное открытие окна с анимацией и показом подвидов.
        /// </summary>
        public virtual async UniTask OpenAsync()
        {
            if (_canvasGroup == null || _rectTransform == null) return;
            _canvasGroup.interactable = false;

            await ViewAnimator.AnimateRectAsync(_rectTransform, _defaultAnchoredPosition, _animation, true, true,
                _canvas.GetCancellationTokenOnDestroy());
            await ShowAllSubViewsAsync();

            _canvasGroup.interactable = true;
        }

        /// <summary>
        /// Асинхронное закрытие окна с анимацией и скрытием подвидов.
        /// </summary>
        public virtual async UniTask CloseAsync()
        {
            if (_canvasGroup == null || _rectTransform == null) return;
            _canvasGroup.interactable = false;

            await HideAllSubViewsAsync();
            await ViewAnimator.AnimateRectAsync(_rectTransform, _defaultAnchoredPosition, _animation, false, true,
                _canvas.GetCancellationTokenOnDestroy());
        }

        /// <summary>
        /// Асинхронно скрыть все подвиды.
        /// </summary>
        public virtual async UniTask HideAllSubViewsAsync()
        {
            if (_subViews == null || _subViews.Count == 0) return;
            await UniTask.WhenAll(_subViews
                .Select(subView => subView?.PlayCloseAsync(_canvas.GetCancellationTokenOnDestroy()))
                .Where(task => task.HasValue)
                .Select(task => task.Value));
        }

        /// <summary>
        /// Асинхронно показать все подвиды.
        /// </summary>
        public virtual async UniTask ShowAllSubViewsAsync()
        {
            if (_subViews == null || _subViews.Count == 0) return;
            await UniTask.WhenAll(_subViews
                .Select(subView => subView?.PlayOpenAsync(_canvas.GetCancellationTokenOnDestroy()))
                .Where(task => task.HasValue)
                .Select(task => task.Value));
        }

        /// <summary>
        /// Синхронно закрыть окно и все подвиды.
        /// </summary>
        public virtual void Close()
        {
            if (_canvasGroup == null) return;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
            if (_subViews == null) return;
            foreach (var subView in _subViews)
                subView?.PlayClose();
        }

        /// <summary>
        /// Синхронно открыть окно и все подвиды.
        /// </summary>
        public virtual void Open()
        {
            if (_canvasGroup == null || _rectTransform == null) return;
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
            _rectTransform.anchoredPosition = Vector2.zero;
            _rectTransform.localScale = Vector3.one;
            if (_subViews == null) return;
            foreach (var subView in _subViews)
                subView?.PlayOpen();
        }
    }
}