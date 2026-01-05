using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.Interfaces.View;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

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
        private readonly Dictionary<Type, AViewBase> _views = new();
        private AViewBase _activeAView;
        public AViewBase ActiveAView => _activeAView;

        private readonly IObjectResolver _resolver;

        public FullScreenViewProvider(ViewRegistry registry, Transform manager, IObjectResolver resolver)
        {
            _registry = registry;
            _fullScreenRoot = manager;
            _resolver = resolver;
        }

        public async UniTask<T> OpenViewAsync<T>() where T : AViewBase
        {
            var type = typeof(T);

            // Если уже открыт нужный тип — ничего не делаем
            if (_activeAView != null && _activeAView.GetType() == type)
            {
                TDebug.Log($"{_nameLog} View {type.Name} is already open, skipping.");
                return (T)_activeAView;
            }

            // Закрываем текущее окно с анимацией
            if (_activeAView != null)
            {
                await _activeAView.CloseAsync();
                _activeAView.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Previous view closed: {_activeAView.name}");
            }

            // Получаем из кэша или создаём
            if (!_views.TryGetValue(type, out var view) || view == null)
            {
                view = _registry.GetPrefab<T>();
                if (view == null)
                    throw new Exception($"{_nameLog} No prefab found for type {type.Name}");
                view = UnityEngine.Object.Instantiate(view, _fullScreenRoot);
                _resolver.InjectGameObject(view.gameObject); // Инъекция зависимостей
                _views[type] = view;
            }

            _activeAView = view;
            _activeAView.transform.SetParent(_fullScreenRoot, false);
            _activeAView.gameObject.SetActive(true);

            await _activeAView.OpenAsync();

            TDebug.Log($"{_nameLog} View opened: {_activeAView.name}");

            return (T)_activeAView;
        }

        public T OpenView<T>(T view) where T : AViewBase
        {
            var type = typeof(T);

            if (_activeAView != null && _activeAView.GetType() == type)
            {
                TDebug.Log($"{_nameLog} View {type.Name} is already open, skipping.");
                return (T)_activeAView;
            }

            // Быстро закрываем текущее окно
            if (_activeAView != null)
            {
                _activeAView.Close();
                _activeAView.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Previous view closed instantly: {_activeAView.name}");
            }

            // Кэшируем, если не было
            if (!_views.ContainsKey(type) || _views[type] == null)
            {
                _views[type] = view;
            }

            _activeAView = view;
            _activeAView.transform.SetParent(_fullScreenRoot, false);
            _activeAView.gameObject.SetActive(true);

            _activeAView.Open();
            TDebug.Log($"{_nameLog} View opened instantly: {_activeAView.name}");

            return (T)_activeAView;
        }

        public async UniTask CloseViewAsync<T>(T view) where T : AViewBase
        {
            if (_activeAView == null)
            {
                TDebug.Log($"{_nameLog} No active view to close.");
                return;
            }

            await _activeAView.CloseAsync();
            _activeAView.gameObject.SetActive(false);
            TDebug.Log($"{_nameLog} View closed: {_activeAView.name}");
            _activeAView = null;
        }

        public void CloseView<T>(T view) where T : AViewBase
        {
            if (_activeAView == null)
            {
                TDebug.Log($"{_nameLog} No active view to close.");
                return;
            }

            _activeAView.Close();
            _activeAView.gameObject.SetActive(false);
            TDebug.Log($"{_nameLog} View closed instantly: {_activeAView.name}");
            _activeAView = null;
        }

        public T GetView<T>() where T : AViewBase
        {
            var type = typeof(T);
            if (_views.TryGetValue(type, out var cached) && cached != null)
                return (T)cached;
            TDebug.Log($"{_nameLog} View of type {type.Name} not found in cache.");
            return null;
        }
    }
}