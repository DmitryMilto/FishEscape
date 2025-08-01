using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Views.Animations;
using __Project.Scripts.NewSystem.Views.Managers;
using __Project.Scripts.NewSystem.Views.SubViews;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Base
{
    public abstract class ViewBase : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog => $"<color=#A6F6E6>[{this.GetType().Name}]</color>";
#else
        protected string _nameLog => $"[{this.GetType().Name}]";
#endif
        [Inject] protected ViewManager _manager;
        
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] private List<SubView> _subViews;
        [SerializeField] private TypeAnimation _animation = TypeAnimation.None;
        private RectTransform _windowRect => this.transform as RectTransform;

        [SerializeField] protected ETypeView _viewType;
        public ETypeView TypeView => _viewType;
        private RectTransform _rect;
        private Vector2 _anchored;

        protected virtual void Awake()
        {
            _rect ??= this.transform as RectTransform;
            _anchored = _rect.anchoredPosition;
        }
#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
        }
#endif 
        // public void InitializeViews(ViewManager manager)
        // {
        //     _manager = manager;
        // }
        public virtual async UniTask OpenAsync()
        {
            _canvasGroup.interactable = false;
            
            // Анимация окна
            await ViewAnimator.AnimateRectAsync(_windowRect,_anchored, _animation, true, true);
            // Запуск анимаций мини-объектов параллельно
            await ShowAllSubViews();
            
            _canvasGroup.interactable = true;
        }

        public virtual async UniTask CloseAsync()
        {
            _canvasGroup.interactable = false;
            
            // Анимация мини-объектов параллельно
            await HideAllSubViews();
            // Анимация окна
            await ViewAnimator.AnimateRectAsync(_windowRect,_anchored, _animation, false, true);
        }
        public virtual async UniTask HideAllSubViews()
        {
            if (_subViews == null) return;
            await UniTask.WhenAll(_subViews.Select(a => a.PlayCloseAsync()));
        }

        public virtual async UniTask ShowAllSubViews()
        {
            if (_subViews == null) return;
            await UniTask.WhenAll(_subViews.Select(a => a.PlayOpenAsync()));
        }

        public virtual void Close()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
            foreach (var subView in _subViews)
                subView.PlayClose();
        }
        public virtual void Open()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;
            _windowRect.anchoredPosition = Vector2.zero;
            _windowRect.localScale = Vector3.one;
            foreach (var subView in _subViews)
                subView.PlayOpen();
        }
    }
}