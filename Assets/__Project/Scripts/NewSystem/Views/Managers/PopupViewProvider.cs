using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Interfaces.View;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.Views.Managers
{
    public class PopupViewProvider : IViewProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        private string _nameLog = $"<color=#97D9C6>[{nameof(PopupViewProvider)}]</color>";
#else
        private string _nameLog = $"[{nameof(PopupViewProvider)}]";
#endif
        private readonly ViewRegistry _registry;
        private readonly Transform _popupRoot;
        private readonly Canvas _shadowCanvas;
        private readonly Canvas _blurCanvas;
        private readonly Stack<PopupBase> _popupStack = new();
        private readonly IObjectResolver _resolver;

        private PopupBase _activePopup;
        private int _baseSortingOrder = 10;
        private int _sortingStep = 2;
        private readonly FullScreenViewProvider _fullScreenProvider;

        public PopupViewProvider(ViewRegistry registry, FullScreenViewProvider fullScreenProvider, Transform popupRoot,
            Canvas shadowCanvas, Canvas blurCanvas, IObjectResolver resolver)
        {
            _registry = registry;
            _fullScreenProvider = fullScreenProvider;
            _popupRoot = popupRoot;
            _shadowCanvas = shadowCanvas;
            _blurCanvas = blurCanvas;
            _resolver = resolver;
        }

        public async UniTask<T> OpenViewAsync<T>() where T : ViewBase
        {
            var type = typeof(T);

            if (_activePopup != null && _activePopup.GetType() == type)
            {
                TDebug.Log($"{_nameLog} Popup {type.Name} is already open, skipping.");
                return (T)(ViewBase)_activePopup;
            }

            // Скрываем subview у активного полноэкранного окна
            if (_fullScreenProvider.ActiveView != null)
                await _fullScreenProvider.ActiveView.HideAllSubViews();

            if (_activePopup != null)
            {
                await _activePopup.HideAllSubViews();
                _popupStack.Push(_activePopup);
                _activePopup.gameObject.SetActive(false);
            }

            var popup = GetView<T>() as PopupBase;
            if (popup == null)
                throw new Exception($"{_nameLog} No prefab found for type {type.Name}");
            
            popup = UnityEngine.Object.Instantiate(popup, _popupRoot);
            _resolver.InjectGameObject(popup.gameObject); // Инъекция зависимостей

            int order = _baseSortingOrder + _popupStack.Count * _sortingStep;
            SetSortingOrder(popup, order);

            _activePopup = popup;
            _activePopup.transform.SetParent(_popupRoot, false);
            _activePopup.gameObject.SetActive(true);

            HandleBackgrounds(_activePopup.Background, order);

            await _activePopup.OpenAsync();

            TDebug.Log($"{_nameLog} Popup opened: {_activePopup.name}");

            return (T)(ViewBase)_activePopup;
        }

        public T OpenView<T>(T view) where T : ViewBase
        {
            var popup = view as PopupBase;
            if (popup == null)
                throw new ArgumentException("View is not a PopupBase!");

            if (_activePopup != null && _activePopup.GetType() == popup.GetType())
            {
                TDebug.Log($"{_nameLog} Popup {popup.name} is already open, skipping.");
                return popup as T;
            }

            // Скрываем subview у активного полноэкранного окна
            if (_fullScreenProvider.ActiveView != null)
                _fullScreenProvider.ActiveView.HideAllSubViews().Forget();

            if (_activePopup != null)
            {
                _activePopup.Close();
                _popupStack.Push(_activePopup);
                _activePopup.gameObject.SetActive(false);
            }

            int order = _baseSortingOrder + _popupStack.Count * _sortingStep;
            SetSortingOrder(popup, order);

            _activePopup = popup;
            _activePopup.transform.SetParent(_popupRoot, false);
            _activePopup.gameObject.SetActive(true);

            HandleBackgrounds(_activePopup.Background, order);

            _activePopup.Open();
            TDebug.Log($"{_nameLog} Popup opened instantly: {_activePopup.name}");

            return popup as T;
        }

        public async UniTask CloseViewAsync<T>(T view) where T : ViewBase
        {
            var popup = view as PopupBase;
            if (popup == null)
            {
                TDebug.Log($"{_nameLog} View to close is not a PopupBase, skipping.");
                return;
            }

            if (_activePopup == popup)
            {
                await _activePopup.HideAllSubViews();
                await _activePopup.CloseAsync();
                _activePopup.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Popup closed: {_activePopup.name}");

                _activePopup = _popupStack.Count > 0 ? _popupStack.Pop() : null;
                if (_activePopup != null)
                {
                    int order = _baseSortingOrder + (_popupStack.Count) * _sortingStep;
                    SetSortingOrder(_activePopup, order);
                    _activePopup.gameObject.SetActive(true);
                    HandleBackgrounds(_activePopup.Background, order);
                    await _activePopup.OpenAsync();
                    TDebug.Log($"{_nameLog} Restored previous popup: {_activePopup.name}");
                }
                else
                {
                    HandleBackgrounds(TypePopupBackground.None, 0);
                    // Возвращаем subview у активного полноэкранного окна
                    if (_fullScreenProvider.ActiveView != null)
                        await _fullScreenProvider.ActiveView.ShowAllSubViews();
                }
            }
            else
            {
                await popup.HideAllSubViews();
                await popup.CloseAsync();
                popup.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Popup closed: {popup.name}");
            }
        }

        public void CloseView<T>(T view) where T : ViewBase
        {
            var popup = view as PopupBase;
            if (popup == null)
            {
                TDebug.Log($"{_nameLog} View to close is not a PopupBase, skipping.");
                return;
            }

            if (_activePopup == popup)
            {
                _activePopup.Close();
                _activePopup.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Popup closed instantly: {_activePopup.name}");

                _activePopup = _popupStack.Count > 0 ? _popupStack.Pop() : null;
                if (_activePopup != null)
                {
                    int order = _baseSortingOrder + (_popupStack.Count) * _sortingStep;
                    SetSortingOrder(_activePopup, order);
                    _activePopup.gameObject.SetActive(true);
                    HandleBackgrounds(_activePopup.Background, order);
                    _activePopup.Open();
                    TDebug.Log($"{_nameLog} Restored previous popup instantly: {_activePopup.name}");
                }
                else
                {
                    HandleBackgrounds(TypePopupBackground.None, 0);
                    // Возвращаем subview у активного полноэкранного окна
                    if (_fullScreenProvider.ActiveView != null)
                        _fullScreenProvider.ActiveView.ShowAllSubViews().Forget();
                }
            }
            else
            {
                popup.Close();
                popup.gameObject.SetActive(false);
                TDebug.Log($"{_nameLog} Popup closed instantly: {popup.name}");
            }
        }

        public T GetView<T>() where T : ViewBase
        {
            return _registry.GetPrefab<T>();
        }

        private void SetSortingOrder(PopupBase popup, int order)
        {
            var canvas = popup.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.overrideSorting = true;
                canvas.sortingOrder = order + 2;
            }
        }

        private void HandleBackgrounds(TypePopupBackground background, int order)
        {
            if (_shadowCanvas != null)
            {
                _shadowCanvas.gameObject.SetActive(background == TypePopupBackground.Shadow ||
                                                   background == TypePopupBackground.ShadowBlur);
                _shadowCanvas.sortingOrder = order;
            }

            if (_blurCanvas != null)
            {
                _blurCanvas.gameObject.SetActive(background == TypePopupBackground.Blur ||
                                                 background == TypePopupBackground.ShadowBlur);
                _blurCanvas.sortingOrder = order + 1;
            }
        }
    }
}