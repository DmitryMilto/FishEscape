using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Game.Spawner;
using _Project.Data.Boosts;
using _Project.Core.Utils;
using _Project.Player;

namespace _Project.Game.Boosts
{
    public class BoostManager : MonoBehaviour
    {
        [SerializeField] private GameObject boostPrefab;
        [SerializeField] private float spawnInterval = 10f;
        [SerializeField] private List<BoostData> availableBoosts;
        [SerializeField] private SpawnZone spawnZone;
        [SerializeField] private PlayerController player;

        private Coroutine _spawnRoutine;
        private bool _spawning;

        public void BeginSpawning()
        {
            if (_spawning) return;

            _spawning = true;
            _spawnRoutine = StartCoroutine(SpawnLoop());
            TDebug.Log("[BoostManager] Begin spawning.");
        }

        public void StopSpawning()
        {
            if (!_spawning) return;

            _spawning = false;
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);

            TDebug.Log("[BoostManager] Stop spawning.");
        }

        private IEnumerator SpawnLoop()
        {
            while (_spawning)
            {
                SpawnBoost();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnBoost()
        {
            if (availableBoosts.Count == 0 || boostPrefab == null || spawnZone == null || player == null)
                return;

            var data = availableBoosts[Random.Range(0, availableBoosts.Count)];
            var position = spawnZone.GetRandomPosition();

            var boostObj = Instantiate(boostPrefab, position, Quaternion.identity);
            var boostComponent = boostObj.GetComponent<BoostObject>();

            if (boostComponent != null)
            {
                boostComponent.Init(data, player.GetStats());
            }

            TDebug.Log("[BoostManager] Spawned boost.");
        }
    }
}
