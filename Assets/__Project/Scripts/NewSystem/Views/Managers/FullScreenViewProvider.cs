using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.Interfaces.View;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Views.Managers
{
    public class FullScreenViewProvider : IViewProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        private string _nameLog = $"<color=#97D9C6>[{nameof(FullScreenViewProvider)}]</color>";
#else
        private string _nameLog = $"[{nameof(FullScreenViewProvider)}]";
#endif
        private readonly ViewRegistry _registry;
        private readonly Transform _fullScreenRoot;
        private readonly Dictionary<Type, ViewBase> _views = new();
        private ViewBase _activeView;
        public ViewBase ActiveView => _activeView;

        public FullScreenViewProvider(ViewRegistry registry, Transform manager)
        {
            TDebug.Log($"{_nameLog} Initializing FullScreenViewProvider...");
            _registry = registry;
            _fullScreenRoot = manager;
        }

        public async UniTask<T> OpenViewAsync<T>() where T : ViewBase
        {
            var type = typeof(T);

            // Если уже открыт нужный тип — ничего не делаем
            if (_activeView != null && _activeView.GetType() == type)
            {
                TDebug.Log($"{_nameLog} View {type.Name} is already open, skipping.");
                return (T)_activeView;
            }

            // Закрываем текущее окно с анимацией
            if (_activeView != null)
            {
                await _activeView.CloseAsync();
                _activeView.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Previous view closed: {_activeView.name}");
            }

            // Получаем из кэша или создаём
            if (!_views.TryGetValue(type, out var view) || view == null)
            {
                view = _registry.GetPrefab<T>();
                if (view == null)
                    throw new Exception($"{_nameLog} No prefab found for type {type.Name}");
                view = UnityEngine.Object.Instantiate(view, _fullScreenRoot);
                _views[type] = view;
            }

            _activeView = view;
            _activeView.transform.SetParent(_fullScreenRoot, false);
            _activeView.gameObject.SetActive(true);

            await _activeView.OpenAsync();

            TDebug.Log($"{_nameLog} View opened: {_activeView.name}");

            return (T)_activeView;
        }

        public T OpenView<T>(T view) where T : ViewBase
        {
            var type = typeof(T);

            if (_activeView != null && _activeView.GetType() == type)
            {
                TDebug.Log($"{_nameLog} View {type.Name} is already open, skipping.");
                return (T)_activeView;
            }

            // Быстро закрываем текущее окно
            if (_activeView != null)
            {
                _activeView.Close();
                _activeView.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Previous view closed instantly: {_activeView.name}");
            }

            // Кэшируем, если не было
            if (!_views.ContainsKey(type) || _views[type] == null)
            {
                _views[type] = view;
            }

            _activeView = view;
            _activeView.transform.SetParent(_fullScreenRoot, false);
            _activeView.gameObject.SetActive(true);

            _activeView.Open();
            TDebug.Log($"{_nameLog} View opened instantly: {_activeView.name}");

            return (T)_activeView;
        }

        public async UniTask CloseViewAsync<T>(T view) where T : ViewBase
        {
            if (_activeView == null)
            {
                TDebug.Log($"{_nameLog} No active view to close.");
                return;
            }

            await _activeView.CloseAsync();
            _activeView.gameObject.SetActive(false);
            TDebug.Log($"{_nameLog} View closed: {_activeView.name}");
            _activeView = null;
        }

        public void CloseView<T>(T view) where T : ViewBase
        {
            if (_activeView == null)
            {
                TDebug.Log($"{_nameLog} No active view to close.");
                return;
            }

            _activeView.Close();
            _activeView.gameObject.SetActive(false);
            TDebug.Log($"{_nameLog} View closed instantly: {_activeView.name}");
            _activeView = null;
        }

        public T GetView<T>() where T : ViewBase
        {
            var type = typeof(T);
            if (_views.TryGetValue(type, out var cached) && cached != null)
                return (T)cached;
            TDebug.Log($"{_nameLog} View of type {type.Name} not found in cache.");
            return null;
        }
    }
}