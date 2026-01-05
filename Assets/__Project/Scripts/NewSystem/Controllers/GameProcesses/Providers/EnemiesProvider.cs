using System.Collections.Generic;
using System.Linq;
using __Project.Scripts.NewSystem.Boosters;
using __Project.Scripts.NewSystem.DataManager.Providers;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using __Project.Scripts.NewSystem.Pools;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public class EnemyManager : BaseGameProvider
    {
        private readonly List<EnemyBase> _enemyPrefabs;
        private readonly List<BoosterBase> _boosterPrefabs;
        private readonly PlayerFishManager _fishProvider;

        private readonly List<AdvancedPoolMono<EnemyBase>> _enemyPools = new();
        private readonly List<AdvancedPoolMono<BoosterBase>> _boosterPools = new();

        private const int MaxObjectsOnLine = 10;
        private const float SpawnInterval = 3.5f;
        private const float SpawnRadius = 1.5f;
        private const float MinDistance = 3.5f;
        private const float DoubleSpawnChance = 0.15f;
        private const float BoosterSpawnChance = 0.25f;

        private float _rightScreenX => ScreenBoundsUtils.GetRightScreenX();
        private float _timer;

        public EnemyManager(Transform spawnPoint, PlayerFishManager fishProvider, List<EnemyBase> enemyPrefabs,
            List<BoosterBase> boosterPrefabs) : base(spawnPoint)
        {
            _enemyPrefabs = enemyPrefabs ?? new List<EnemyBase>();
            _boosterPrefabs = boosterPrefabs ?? new List<BoosterBase>();
            _fishProvider = fishProvider;
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var enemy in _enemyPrefabs)
                _enemyPools.Add(new AdvancedPoolMono<EnemyBase>(enemy, MaxObjectsOnLine, _spawnPoint));
            foreach (var booster in _boosterPrefabs)
                _boosterPools.Add(new AdvancedPoolMono<BoosterBase>(booster, MaxObjectsOnLine, _spawnPoint));
        }

        public override void NewGame()
        {
            isGameOver = false;
            DestroyAllObjects();
        }
        public override void GameOverGame()
        {
            isGameOver = true;
            DestroyAllObjects();
        }
        public override void ResumeGame()
        {
            isGameOver = false;
            _timer = 0;
            DestroyAllObjects();
        }

        public override void Update()
        {
            if (isPauseGame || isGameOver) return;
            _timer += Time.deltaTime;
            if (_timer >= SpawnInterval && GetActiveObjectsCount() < MaxObjectsOnLine)
            {
                int spawnCount = 1;
                if (Random.value < DoubleSpawnChance && GetActiveObjectsCount() <= MaxObjectsOnLine - 2)
                    spawnCount = 2;

                for (int i = 0; i < spawnCount; i++)
                    SpawnEnemyIfPossible();

                if (Random.value < BoosterSpawnChance)
                    SpawnBoosterIfPossible();

                _timer = 0;
            }
        }

        public override void PauseGame(bool isPause)
        {
            base.PauseGame(isPause);
            SetObjectsActive(!isPause);
        }

        public override void DestroyProvider()
        {
            DestroyAllObjects();
            DisposePools();
        }

        private void DisposePools()
        {
            // Для предотвращения утечек памяти при уничтожении пулов
            _enemyPools.Clear();
            _boosterPools.Clear();
        }

        // C#
        private void SpawnEnemyIfPossible()
        {
            if (_enemyPools.Count == 0) return;
            var pool = _enemyPools[Random.Range(0, _enemyPools.Count)];
            var prefab = pool.Prefab;

            // 1. Выбор случайной скорости
            var minSpeed = prefab.MinSpeed;
            var maxSpeed = prefab.MaxSpeed;
            var speed = Random.Range(minSpeed, maxSpeed) + Random.Range(-0.1f, 0.1f);

            // 2. Позиция спавна вне зоны перед игроком
            var playerY = _fishProvider?.CurrentFish?.transform.position.y ?? 0f;
            var spawnY = 0f;
            var attempts = 0;
            do
            {
                spawnY = Random.Range(playerY - SpawnRadius, playerY + SpawnRadius);
                attempts++;
            } while (!IsPositionValid(prefab, spawnY) && attempts < 5);

            if (!IsPositionValid(prefab, spawnY))
            {
                spawnY = Random.Range(-4f, 4f);
            }

            var enemy = pool.GetOrReuseElement(new Vector3(_rightScreenX, spawnY, 0));
            if (enemy == null) return;
            enemy.SetSpeed(speed);
            enemy.OnSpawn(enemy.transform.position);

            // 3. Проверка на столкновение и смещение
            if (!IsCollidingWithOtherEnemies(enemy)) return;
            var offset = Random.value > 0.5f ? 1f : -1f;
            enemy.transform.position += new Vector3(0, offset, 0);
        }

        private bool IsCollidingWithOtherEnemies(EnemyBase enemy)
        {
            var activeEnemies = _enemyPools.SelectMany(p => p.Pool)
                .Where(e => e.gameObject.activeInHierarchy && e != enemy)
                .ToList();

            foreach (var other in activeEnemies)
            {
                if (enemy.GetComponent<Collider2D>().bounds.Intersects(other.GetComponent<Collider2D>().bounds))
                    return true;
            }
            return false;
        }


        private float? GetFreeYPositionNear(EnemyBase prefab, float centerY, float radius)
        {
            for (var attempt = 0; attempt < 5; attempt++)
            {
                var y = Random.Range(centerY - radius, centerY + radius);
                if (IsPositionValid(prefab, y))
                    return y;
            }
            return null;
        }

        private float? GetFreeYPositionAnywhere(EnemyBase prefab)
        {
            for (var attempt = 0; attempt < 10; attempt++)
            {
                var y = Random.Range(-4f, 4f);
                if (IsPositionValid(prefab, y))
                    return y;
            }
            return null;
        }

        private bool IsPositionValid(EnemyBase prefab, float y)
        {
            float halfHeight = prefab.GetComponent<Collider2D>()?.bounds.extents.y ?? 0.5f;
            Vector2 newPos = new Vector2(_rightScreenX, y);
            var activeEnemies = _enemyPools.SelectMany(p => p.Pool)
                .Where(e => e.gameObject.activeInHierarchy)
                .ToList();

            foreach (var other in activeEnemies)
            {
                float otherHalf = other.GetComponent<Collider2D>()?.bounds.extents.y ?? 0.5f;
                Vector2 otherPos = new Vector2(other.transform.position.x, other.transform.position.y);
                float minDist = halfHeight + otherHalf + MinDistance;
                if (Vector2.Distance(newPos, otherPos) < minDist)
                    return false;
            }

            foreach (var other in activeEnemies)
            {
                if (Mathf.Abs(other.transform.position.y - y) < 0.1f)
                {
                    if (prefab.Speed > other.Speed && _rightScreenX < other.transform.position.x)
                        return false;
                }
            }
            return true;
        }

        private void SpawnBoosterIfPossible()
        {
            if (_boosterPools.Count == 0) return;
            var pool = _boosterPools[Random.Range(0, _boosterPools.Count)];
            float y = Random.Range(-4f, 4f);
            var booster = pool.GetOrReuseElement(new Vector3(_rightScreenX, y, 0));
            booster?.OnSpawn(booster.transform.position);
        }

        private int GetActiveObjectsCount()
        {
            int count = 0;
            foreach (var pool in _enemyPools)
                count += pool.ActiveCount();
            foreach (var pool in _boosterPools)
                count += pool.ActiveCount();
            return count;
        }

        private void DestroyAllObjects()
        {
            foreach (var pool in _enemyPools)
                pool.DestroyAll();
            foreach (var pool in _boosterPools)
                pool.DestroyAll();
        }

        private void SetObjectsActive(bool active)
        {
            foreach (var pool in _enemyPools)
                foreach (var enemy in pool.Pool)
                    if (enemy.gameObject.activeInHierarchy)
                        enemy.enabled = active;
            foreach (var pool in _boosterPools)
                foreach (var booster in pool.Pool)
                    if (booster.gameObject.activeInHierarchy)
                        booster.enabled = active;
        }
    }
}