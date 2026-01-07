using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Base;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Fishes
{
    public abstract class DBBaseFishes<TPlayer> : ScriptableObject where TPlayer : BaseFish
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=green>[{nameof(DBBaseFishes<TPlayer>)}]</color>";
#else
        protected string _nameLog = $"[{nameof(DBBaseFishes<TPlayer>)}]";
#endif
        
        protected Dictionary<TypeOceans, Dictionary<ENamesFish,TPlayer>> _cache = new Dictionary<TypeOceans, Dictionary<ENamesFish, TPlayer>>();
        [SerializeField] protected List<TPlayer> fishes;
        
        public void SetFishes(List<TPlayer> fishes) => this.fishes = fishes;
        
        public TPlayer GetFish(ENamesFish name, TypeOceans ocean)
        {
            if (_cache.TryGetValue(ocean, out var cacheFish) && cacheFish.TryGetValue(name, out var cachedFish))
            {
                TDebug.Log($"{_nameLog}: Retrieving fish '{name}' from ocean '{ocean}' from cache.");
                return cachedFish;
            }

            var fish = fishes.Find(f => f.Name == name && f.IsInOcean(ocean));
            if (fish == null)
            {
                TDebug.LogError($"{_nameLog}: Fish '{name}' not found in ocean '{ocean}'.");
                return null;
            }

            if (!_cache.TryGetValue(ocean, out cacheFish))
                _cache[ocean] = cacheFish = new Dictionary<ENamesFish, TPlayer>();

            TDebug.Log($"{_nameLog}: Adding fish '{name}' to cache for ocean '{ocean}'.");
            cacheFish[name] = fish;

            return fish;
        }
    }
}