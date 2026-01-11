using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Interfaces.View;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Managers
{
    public class ViewManager : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        private string _logName = $"<color=#97D9C6>[{nameof(ViewManager)}]</color>";
#else
        private string _logName = $"[{nameof(ViewManager)}]";
#endif
        [Inject] private ViewRegistry _registry;
        [Inject] private IObjectResolver _resolver; // Добавить это поле

        [SerializeField] private Transform _fullScreenRoot;
        [SerializeField] private Transform _popupRoot;
        [SerializeField] private Canvas _shadowCanvas;
        [SerializeField] private Canvas _blurCanvas;

        private FullScreenViewProvider _fullScreenProvider;
        private PopupViewProvider _popupProvider;
        private SplashProvider _splashProvider;

        private Dictionary<Type, AViewBase> _cache = new();

        public void Awake()
        {
            _fullScreenProvider = new FullScreenViewProvider(_registry, _fullScreenRoot, _resolver);
            _popupProvider = new PopupViewProvider(_registry, _fullScreenProvider, _popupRoot, _shadowCanvas,
                _blurCanvas, _resolver);
            // _splashProvider = new SplashProvider(_registry, _resolver);
        }

        // Открытие с анимацией
        public async UniTask<T> OpenViewAsync<T>() where T : AViewBase
        {
            var type = typeof(T);
            var prefab = _registry.GetPrefab<T>();
            if (prefab == null)
                throw new Exception($"{_logName} No prefab found for type {type.Name}");

            switch (prefab.ViewType)
            {
                case ETypeView.FullScreen:
                    var fullScreen = await _fullScreenProvider.OpenViewAsync<T>();
                    _cache[type] = fullScreen;
                    // fullScreen.InitializeViews(this);
                    return fullScreen as T;
                case ETypeView.Popup:
                    var popup = await _popupProvider.OpenViewAsync<T>();
                    _cache[type] = popup;
                    // popup.InitializeViews(this);
                    return popup as T;
                default:
                    throw new Exception($"{_logName} Unknown view type: {prefab.ViewType}");
            }
        }

        // Быстрое открытие (без анимации)
        public T OpenView<T>(T view) where T : AViewBase
        {
            var type = typeof(T);
            if (view == null)
                throw new ArgumentNullException($"{_logName} View is null");

            switch (view.ViewType)
            {
                case ETypeView.FullScreen:
                    var fullScreen = _fullScreenProvider.OpenView(view);
                    _cache[type] = fullScreen;
                    // fullScreen.InitializeViews(this);
                    return fullScreen as T;
                case ETypeView.Popup:
                    var popup = _popupProvider.OpenView(view);
                    _cache[type] = popup;
                    // popup.InitializeViews(this);
                    return popup as T;
                default:
                    throw new Exception($"{_logName} Unknown view type: {view.ViewType}");
            }
        }

        public T OpenView<T>() where T : AViewBase
        {
            var type = typeof(T);
            var prefab = _registry.GetPrefab<T>();
            if (prefab == null)
                throw new Exception($"{_logName} No prefab found for type {type.Name}");

            switch (prefab.ViewType)
            {
                case ETypeView.FullScreen:
                    var fullScreen = _fullScreenProvider.OpenView(prefab as T);
                    _cache[type] = fullScreen;
                    // fullScreen.InitializeViews(this);
                    return fullScreen as T;
                case ETypeView.Popup:
                    var popup = _popupProvider.OpenView(prefab as T);
                    _cache[type] = popup;
                    // popup.InitializeViews(this);
                    return popup as T;
                default:
                    throw new Exception($"{_logName} Unknown view type: {prefab.ViewType}");
            }
        }

        public async UniTask CloseViewAsync<T>(T view = null) where T : AViewBase
        {
            var type = typeof(T);
            if (view == null)
            {
                if (!_cache.ContainsKey(type) || view == null)
                    throw new ArgumentNullException($"{_logName} View is null and not found in cache");
                else
                {
                    view = _cache[type] as T;
                }
            }

            if (view == null)
            {
                TDebug.Log($"{_logName} View is null, cannot close.");
                return;
            }

            switch (view.ViewType)
            {
                case ETypeView.FullScreen:
                    await _fullScreenProvider.CloseViewAsync(view);
                    break;
                case ETypeView.Popup:
                    await _popupProvider.CloseViewAsync(view);
                    break;
                default:
                    throw new Exception($"{_logName} Unknown view type: {view.ViewType}");
            }
        }

        public void CloseView<T>(T view = null) where T : AViewBase
        {
            var type = typeof(T);
            if (view == null)
            {
                if (!_cache.ContainsKey(type) || view == null)
                    throw new ArgumentNullException($"{_logName} View is null and not found in cache");
                else
                {
                    view = _cache[type] as T;
                }
            }

            if (view == null)
            {
                TDebug.Log($"{_logName} View is null, cannot close.");
                return;
            }

            switch (view.ViewType)
            {
                case ETypeView.FullScreen:
                    _fullScreenProvider.CloseView(view);
                    break;
                case ETypeView.Popup:
                    _popupProvider.CloseView(view);
                    break;
                default:
                    throw new Exception($"{_logName} Unknown view type: {view.ViewType}");
            }
        }

        public T GetView<T>() where T : AViewBase
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var cached) && cached != null)
                return (T)cached;

            // Пробуем получить у провайдеров
            var fullScreen = _fullScreenProvider?.GetView<T>();
            if (fullScreen != null)
                return fullScreen;

            var popup = _popupProvider?.GetView<T>();
            if (popup != null)
                return popup;

            return null;
        }
    }
}