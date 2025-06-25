using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Views.Base;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.View
{
    [CreateAssetMenu(fileName = "ViewRegistry",menuName = "View/ViewRegistry")]
    public class ViewRegistry : ScriptableObject
    {
        #if UNITY_EDITOR && ALL_DEBUG
        private string _nameLog = $"<color=#4384AE>[{nameof(ViewRegistry)}]</color>";
        #else
        private string _nameLog = $"[{nameof(ViewRegistry)}]";
        #endif
        
        public ViewBase[] entries;
        private Dictionary<Type, ViewBase> _cache = new Dictionary<Type, ViewBase>();

        public T GetPrefab<T>() where T : ViewBase
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var cached))
            {
                TDebug.Log($"{_nameLog}: Returning cached prefab of type {type.Name}");
                return cached as T;
            }

            foreach (var prefab in entries)
            {
                if (prefab != null && prefab.GetType() == type)
                {
                    TDebug.Log($"{_nameLog}: Found prefab of type {type.Name}");
                    _cache[type] = prefab;
                    return prefab as T;
                }
            }

            TDebug.LogError($"{_nameLog}: No prefab found for type {type.Name}");
            return null;
        }
    }
}