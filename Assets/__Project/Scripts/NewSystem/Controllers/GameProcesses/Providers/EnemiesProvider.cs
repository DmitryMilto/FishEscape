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
    public class EnemiesProvider : BaseGameProvider
    {
        private readonly List<EnemyBase> _enemies;
        private readonly List<BoosterBase> _boosters;
        private readonly FishProvider _fishProvider;

        private readonly List<AdvancedPoolMono<EnemyBase>> _enemyPools = new();
        private readonly List<AdvancedPoolMono<BoosterBase>> _boosterPools = new();

        private int maxObjectsOnLine => 10;
        private float spawnInterval => 3.5f;
        private float spawnRadius => 1.5f;
        private float minDistance => 3.5f; // минимальное расстояние между врагами по Y
        private float doubleSpawnChance => 0.15f; // 15% шанс на двойной спавн
        private float boosterSpawnChance => 0.25f;

        private float rightScreenX => ScreenBoundsUtils.GetRightScreenX();
        private float timer;

        public EnemiesProvider(Transform spawnPoint, FishProvider fish, List<EnemyBase> enemies,
            List<BoosterBase> boosters) : base(
            spawnPoint)
        {
            _enemies = enemies ?? new List<EnemyBase>();
            _boosters = boosters ?? new List<BoosterBase>();
            _fishProvider = fish;
            InitializePools();
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
            timer = 0;
            DestroyAllObjects();
        }

        // В Update:
        public override void Update()
        {
            if (isPauseGame) return;
            if(isGameOver) return;
            timer += Time.deltaTime;
            if (timer >= spawnInterval && ActiveObjectsCount() < maxObjectsOnLine)
            {
                int spawnCount = 1;
                if (Random.value < doubleSpawnChance && ActiveObjectsCount() <= maxObjectsOnLine - 2)
                    spawnCount = 2;

                for (int i = 0; i < spawnCount; i++)
                    TrySpawnEnemy();

                // Спавн буста с шансом (например, 30%)
                if (Random.value < boosterSpawnChance)
                    TrySpawnBooster();

                timer = 0;
            }
        }

        public override void PauseGame(bool isPause)
        {
            base.PauseGame(isPause);
            SetEnemiesActive(!isPause);
        }

        public override void DestroyProvider() => DestroyAllObjects();

        private void InitializePools()
        {
            foreach (var enemy in _enemies)
                _enemyPools.Add(new AdvancedPoolMono<EnemyBase>(enemy, maxObjectsOnLine, _spawnPoint));
            foreach (var booster in _boosters)
                _boosterPools.Add(new AdvancedPoolMono<BoosterBase>(booster, maxObjectsOnLine, _spawnPoint));
        }

        void TrySpawnEnemy()
        {
            var pool = _enemyPools[Random.Range(0, _enemyPools.Count)];
            var prefab = pool.Prefab;
            var playerY = _fishProvider?.CurrentFish?.transform.position.y ?? 0f;

            // Сначала пробуем рядом с игроком
            float? y = GetFreeYPositionNear(prefab, playerY, spawnRadius) ?? GetFreeYPositionAnywhere(prefab);

            if (y.HasValue)
            {
                var enemy = pool.GetOrReuseElement(new Vector3(rightScreenX, y.Value, 0));
                enemy.OnSpawn(enemy.transform.position);
            }
            else
            {
                // Если не нашли место — спавним за экраном по X
                var yOut = GetFreeYPositionAnywhere(prefab);
                if (!yOut.HasValue) return;
                var offsetX = 2f; // смещение за экраном
                var enemy = pool.GetOrReuseElement(new Vector3(rightScreenX + offsetX, yOut.Value, 0));
                enemy.OnSpawn(enemy.transform.position);
            }
        }
        float? GetFreeYPositionNear(EnemyBase prefab, float centerY, float radius)
        {
            for (var attempt = 0; attempt < 5; attempt++)
            {
                var y = Random.Range(centerY - radius, centerY + radius);
                if (IsPositionValid(prefab, y))
                    return y;
            }
            return null;
        }

        float? GetFreeYPositionAnywhere(EnemyBase prefab)
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
            Vector2 newPos = new Vector2(rightScreenX, y);
            var activeEnemies = _enemyPools.SelectMany(p => p.Pool)
                .Where(e => e.gameObject.activeInHierarchy)
                .ToList();

            foreach (var other in activeEnemies)
            {
                float otherHalf = other.GetComponent<Collider2D>()?.bounds.extents.y ?? 0.5f;
                Vector2 otherPos = new Vector2(other.transform.position.x, other.transform.position.y);
                float minDist = halfHeight + otherHalf + minDistance;
                if (Vector2.Distance(newPos, otherPos) < minDist)
                    return false;
            }

            // Проверка: не догоняет ли враг другого на линии (по Y с допуском)
            foreach (var other in activeEnemies)
            {
                if (Mathf.Abs(other.transform.position.y - y) < 0.1f)
                {
                    if (prefab.Speed > other.Speed && rightScreenX < other.transform.position.x)
                        return false;
                }
            }
            return true;
        }
        // Спавн бустов (без ограничений)
        void TrySpawnBooster()
        {
            if (_boosterPools.Count == 0) return;
            var pool = _boosterPools[Random.Range(0, _boosterPools.Count)];
            float y = Random.Range(-4f, 4f);
            var booster = pool.GetOrReuseElement(new Vector3(rightScreenX, y, 0));
            booster.OnSpawn(booster.transform.position);
        }

        int ActiveObjectsCount()
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

        private void SetEnemiesActive(bool active)
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