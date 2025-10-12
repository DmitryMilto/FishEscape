using UnityEngine;
using System.Collections.Generic;
using _Project.Data;
using _Project.Game.Enemies;
using _Project.Core.Utils;
using _Project.Data.Enemies;

namespace _Project.Game.Spawner
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnParent;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector2 verticalRange = new Vector2(-4f, 4f);
        [SerializeField] private float minDistance = 1.5f;

        private List<EnemyController> _activeEnemies = new();

        public void SpawnEnemy(EnemyData data)
        {
            Vector3 spawnPos = GetValidSpawnPosition(data.size);
            GameObject go = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, spawnParent);
            var ctrl = go.GetComponent<EnemyController>();
            ctrl.Init(data);
            _activeEnemies.Add(ctrl);
        }

        private Vector3 GetValidSpawnPosition(float radius)
        {
            Vector3 pos;
            int attempts = 10;
            do
            {
                float y = Random.Range(verticalRange.x, verticalRange.y);
                pos = new Vector3(Camera.main.transform.position.x + 10f, y, 0f);

                bool overlaps = false;
                foreach (var e in _activeEnemies)
                {
                    if (e == null) continue;
                    if (Vector3.Distance(e.transform.position, pos) < e.Radius + radius + minDistance)
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (!overlaps) break;
            } while (--attempts > 0);

            return pos;
        }
    }
}