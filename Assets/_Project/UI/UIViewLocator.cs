using System;
using System.Collections.Generic;
using UnityEngine;
using _Project.UI.Panels;
using _Project.Core.Utils;

namespace _Project.UI
{
    public class UIViewLocator : MonoBehaviour
    {
        [SerializeField] private EndLevelPanel endLevelPanel;
        public EndLevelPanel EndLevelPanel => endLevelPanel;
        
        private readonly Dictionary<Type, UIPanelBase> _views = new();

        private void Awake()
        {
            RegisterAllViews();
        }

        private void RegisterAllViews()
        {
            var panels = GetComponentsInChildren<UIPanelBase>(true);
            foreach (var panel in panels)
            {
                var type = panel.GetType();
                if (_views.ContainsKey(type))
                {
                    TDebug.LogWarning($"[UIViewLocator] Duplicate panel type: {type.Name} — skipping.");
                    continue;
                }

                _views.Add(type, panel);
                TDebug.Log($"[UIViewLocator] Registered panel: {type.Name}");
            }
        }

        public void Register<T>(T panel) where T : UIPanelBase
        {
            var type = typeof(T);
            if (_views.ContainsKey(type))
            {
                TDebug.LogWarning($"[UIViewLocator] Panel already registered: {type.Name}");
                return;
            }

            _views.Add(type, panel);
            TDebug.Log($"[UIViewLocator] Manually registered panel: {type.Name}");
        }

        public T Get<T>() where T : UIPanelBase
        {
            var type = typeof(T);
            if (_views.TryGetValue(type, out var panel))
                return (T)panel;

            TDebug.LogError($"[UIViewLocator] Panel not found: {type.Name}");
            return null;
        }

        public bool TryGet<T>(out T panel) where T : UIPanelBase
        {
            if (_views.TryGetValue(typeof(T), out var basePanel))
            {
                panel = basePanel as T;
                return true;
            }

            panel = null;
            return false;
        }
    }
}