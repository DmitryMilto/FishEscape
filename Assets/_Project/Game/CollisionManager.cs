using UnityEngine;
using _Project.Game.Player;
using _Project.Game.Enemies;

namespace _Project.Game
{
    public class CollisionManager : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyController enemy))
            {
                // TODO: урон, эффекты и т.д.
            }
        }
    }
}