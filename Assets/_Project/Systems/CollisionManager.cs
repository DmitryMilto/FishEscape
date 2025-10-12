using System.Collections.Generic;
using UnityEngine;
using _Project.Game.Player;
using _Project.Game.Enemies;
using _Project.Game.Boosts;
using _Project.Core.Utils;

namespace _Project.Systems
{
    public class CollisionManager : MonoBehaviour
    {
        private readonly List<Collider2D> overlapResults = new(10);
        private ContactFilter2D contactFilter;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask boostLayer;

        private void Awake()
        {
            contactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                useTriggers = true
            };
        }

        public void CheckCollisions(PlayerController player)
        {
            contactFilter.layerMask = enemyLayer;
            int hits = Physics2D.OverlapCollider(player.Collider, contactFilter, overlapResults);

            for (int i = 0; i < hits; i++)
            {
                var enemy = overlapResults[i].GetComponent<EnemyController>();
                if (enemy != null)
                {
                    TDebug.Log($"[CollisionManager] Player hit enemy: {enemy.name}");
                    player.TakeDamage(1);
                }
            }

            contactFilter.layerMask = boostLayer;
            hits = Physics2D.OverlapCollider(player.Collider, contactFilter, overlapResults);

            for (int i = 0; i < hits; i++)
            {
                var boost = overlapResults[i].GetComponent<IBoostEffect>();
                if (boost != null)
                {
                    TDebug.Log($"[CollisionManager] Player picked boost: {boost.GetType().Name}");
                    boost.Apply(player.FishStats);
                }
            }
        }
    }
}