using UnityEngine;

namespace __Project.Scripts.NewSystem.Interfaces.Gameplays
{
    public interface ISpawnable
    {
        void OnSpawn(Vector3 position);
        void OnDespawn();
    }
}