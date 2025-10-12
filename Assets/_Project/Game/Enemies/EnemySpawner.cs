using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Project.Game.Spawner;
using _Project.Core.Utils;
using _Project.Data.Enemies;

namespace _Project.Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private float spawnInterval = 2f;
        [SerializeField] private List<EnemyData> possibleEnemies;
        [SerializeField] private SpawnZone spawnZone;

        private List<EnemyController> _activeEnemies = new();
        private Coroutine _spawnRoutine;
        private bool _isSpawning;

        public void BeginSpawning()
        {
            if (_isSpawning) return;

            _isSpawning = true;
            _spawnRoutine = StartCoroutine(SpawnLoop());
            TDebug.Log("[EnemySpawner] Begin spawning.");
        }

        public void StopSpawning()
        {
            if (!_isSpawning) return;

            _isSpawning = false;
            if (_spawnRoutine != null)
                StopCoroutine(_spawnRoutine);

            foreach (var enemy in _activeEnemies)
            {
                if (enemy != null) Destroy(enemy.gameObject);
            }

            _activeEnemies.Clear();
            TDebug.Log("[EnemySpawner] Stop spawning and cleared enemies.");
        }

        private IEnumerator SpawnLoop()
        {
            while (_isSpawning)
            {
                SpawnRandomEnemy();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void SpawnRandomEnemy()
        {
            if (possibleEnemies == null || possibleEnemies.Count == 0 || enemyPrefab == null) return;

            var data = possibleEnemies[Random.Range(0, possibleEnemies.Count)];
            var pos = spawnZone.GetRandomPosition();

            var enemyObj = Instantiate(enemyPrefab, pos, Quaternion.identity);
            var enemyCtrl = enemyObj.GetComponent<EnemyController>();
            enemyCtrl.Init(data);

            _activeEnemies.Add(enemyCtrl);
        }
    }
}
