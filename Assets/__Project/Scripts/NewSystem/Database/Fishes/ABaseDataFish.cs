using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Base;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Fishes
{
    public abstract class ABaseDataFish<TObject> : ScriptableObject where TObject : IOceanFish
    {
        [SerializeField] private List<TObject> _objects;

        public TObject GetObject(TypeOceans ocean)
        {
            var obj = _objects.Find(o => o.IsInOcean(ocean));
            TDebug.Log(
                $"[{nameof(ABaseDataFish<TObject>)}]: Retrieving object for ocean '{ocean}': {(obj != null ? "Found" : "Not Found")}");
            return obj ?? default;
        }

        public List<TObject> GetAllObjects()
        {
            TDebug.Log($"[{nameof(ABaseDataFish<TObject>)}]: Retrieving all objects. Total count: {_objects.Count}");
            return _objects;
        }

        public List<TObject> GetAllObjectsByOcean(TypeOceans ocean)
        {
            var objs = _objects.FindAll(o => o.IsInOcean(ocean));
            TDebug.Log(
                $"[{nameof(ABaseDataFish<TObject>)}]: Retrieving all objects for ocean '{ocean}'. Found count: {objs.Count}");
            return objs;
        }
    }
}