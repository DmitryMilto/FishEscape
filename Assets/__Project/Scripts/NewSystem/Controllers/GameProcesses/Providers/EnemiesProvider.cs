using System.Collections.Generic;
using __Project.Scripts.NewSystem.Boosters;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using __Project.Scripts.NewSystem.Fishes.Players;
using __Project.Scripts.NewSystem.Pools;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public class EnemiesProvider : BaseGameProvider
    {
        private readonly List<EnemyBase> _enemies;
        private readonly List<BoosterBase> _boosters;
        
        private List<AdvancedPoolMono<EnemyBase>> enemyPools = new();
        private List<AdvancedPoolMono<BoosterBase>> boosterPools = new();
        
        private int maxObjectsOnLine = 3;
        private float spawnInterval = 2f;
        
        private float rightScreenX => ScreenBoundsUtils.GetRightScreenX();
        private float timer;
        public EnemiesProvider(Transform spawnPoint, List<EnemyBase> enemies, List<BoosterBase> boosters) : base(spawnPoint)
        {
            _enemies = enemies ?? new List<EnemyBase>();
            _boosters = boosters ?? new List<BoosterBase>();
            InitializePools();
        }

        public override void NewGame()
        {
            throw new System.NotImplementedException();
        }

        public override void GameOverGame()
        {
            DestroyAllObjects();
        }

        public override void ResumeGame()
        {
            DestroyAllObjects();
        }

        public override void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval && ActiveObjectsCount() < maxObjectsOnLine)
            {
                SpawnRandomObject();
                timer = 0;
            }
        }

        public override void DestroyProvider()
        {
            DestroyAllObjects();
        }

        private void InitializePools()
        {
            foreach (var enemy in _enemies)
            {
                var pool = new AdvancedPoolMono<EnemyBase>(enemy, maxObjectsOnLine, _spawnPoint);
                enemyPools.Add(pool);
            }

            foreach (var booster in _boosters)
            {
                var pool = new AdvancedPoolMono<BoosterBase>(booster, maxObjectsOnLine, _spawnPoint);
                boosterPools.Add(pool);
            }
        }
        void SpawnRandomObject()
        {
            // Пример: 50% враг, 50% бустер
            if (Random.value > 0f)
            {
                var pool = enemyPools[Random.Range(0, enemyPools.Count)];
                var enemy = pool.GetOrReuseElement(new Vector3(rightScreenX, Random.Range(-4f, 4f), 0));
                enemy.OnSpawn(enemy.transform.position);
            }
            else
            {
                var pool = boosterPools[Random.Range(0, boosterPools.Count)];
                var enemy = pool.GetOrReuseElement(new Vector3(rightScreenX, Random.Range(-4f, 4f), 0));
                enemy.OnSpawn(enemy.transform.position);
            }
        }

        int ActiveObjectsCount()
        {
            int count = 0;
            foreach (var pool in enemyPools)
                count += pool.ActiveCount();
            foreach (var pool in boosterPools)
                count += pool.ActiveCount();
            return count;
        }
        private void DestroyAllObjects()
        {
            foreach (var pool in enemyPools)
                pool.DestroyAll();
            foreach (var pool in boosterPools)
                pool.DestroyAll();
        }
    }
}