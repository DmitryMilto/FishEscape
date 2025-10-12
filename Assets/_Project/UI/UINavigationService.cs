using System;
using System.Collections.Generic;
using UnityEngine;
using _Project.UI.Panels;
using _Project.Core.Utils;

namespace _Project.UI
{
    public class UINavigationService
    {
        private readonly UIViewLocator _viewLocator;
        private readonly Stack<UIPanelBase> _panelStack = new();

        public UINavigationService(UIViewLocator viewLocator)
        {
            _viewLocator = viewLocator;
        }

        public void Open<T>() where T : UIPanelBase
        {
            var panel = _viewLocator.Get<T>();
            if (panel == null)
            {
                TDebug.LogError($"[UINavigationService] Can't find panel: {typeof(T).Name}");
                return;
            }

            if (_panelStack.Count > 0)
            {
                var current = _panelStack.Peek();
                current.Hide();
            }

            panel.Show();
            _panelStack.Push(panel);
            TDebug.Log($"[UINavigationService] Opened panel: {typeof(T).Name}");
        }

        public void CloseCurrent()
        {
            if (_panelStack.Count == 0)
            {
                TDebug.LogWarning("[UINavigationService] No panel to close");
                return;
            }

            var panel = _panelStack.Pop();
            panel.Hide();

            if (_panelStack.Count > 0)
            {
                _panelStack.Peek().Show();
            }

            TDebug.Log($"[UINavigationService] Closed panel: {panel.GetType().Name}");
        }

        public void CloseAll()
        {
            while (_panelStack.Count > 0)
            {
                var panel = _panelStack.Pop();
                panel.Hide();
            }

            TDebug.Log("[UINavigationService] All panels closed");
        }

        public bool HasOpenedPanels => _panelStack.Count > 0;
    }
}