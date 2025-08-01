using System.Collections.Generic;
using __Project.Scripts.NewSystem.Boosters;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Players.Providers
{
    public class FishCollisionProvider
    {
        private readonly PlayerFishBase _fish;
        private readonly HashSet<EnemyBase> _currentEnemies = new();

        public bool IsTouchingEnemy => _currentEnemies.Count > 0;
        public IEnumerable<EnemyBase> CurrentEnemies => _currentEnemies;

        public FishCollisionProvider(PlayerFishBase fish)
        {
            _fish = fish;
        }
        public void HandleTriggerEnter(Collider2D other)
        {
            if (other.TryGetComponent<EnemyBase>(out var enemy))
            {
                _currentEnemies.Add(enemy);
                OnEnemyCollision(enemy);
            }
            if (other.TryGetComponent<BoosterBase>(out var booster))
            {
                TDebug.Log($"{_fish.name} collided with booster: {booster.name}");
                BoosterCollider(booster);
            }
        }

        public virtual void OnEnemyCollision(EnemyBase enemy)
        {
            if (_fish.IsInvincible) return;

            _fish.CurrentLives--;
            if (_fish.CurrentLives < 0)
            {
                GlobalEventsManager.Death();
            }
            else
            {
                GlobalEventsManager.RemoveLife();
                _fish.StartInvincibilityAsync().Forget();
            }
        }
        public void HandleTriggerExit(Collider2D other)
        {
            if (other.TryGetComponent<EnemyBase>(out var enemy))
            {
                _currentEnemies.Remove(enemy);
                _fish.OnEnemyExit(enemy);
            }
        }

        private void BoosterCollider(BoosterBase booster)
        {
            booster.ApplyEffect(_fish);
            booster.OnPickup();
        }
    }
}