using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database
{
    [CreateAssetMenu(fileName = "EnemiesFish", menuName = "Database/Fishes/Enemies", order = 2)]
    public class DBEnemiesFish : DBBaseFishes<EnemyBase>
    {
        public List<EnemyBase> GetAllEnemiesByOceans(TypeOceans ocean)
        {
            if (_cache.ContainsKey(ocean))
            {
                TDebug.Log($"{_nameLog}: Retrieving all enemies from ocean '{ocean}' from cache.");
                return new List<EnemyBase>(_cache[ocean].Values);
            }

            TDebug.Log($"{_nameLog}: No cached enemies found for ocean '{ocean}', returning all fishes.");
            return GetAllFishes();
        }

        private List<EnemyBase> GetAllFishes()
        {
            List<EnemyBase> allFishes = new List<EnemyBase>();
            foreach (var fish in fishes)
            {
                if (fish is EnemyBase enemy)
                {
                    allFishes.Add(enemy);
                }
            }

            return allFishes;
        }
    }
}