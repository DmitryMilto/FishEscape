using System.Collections.Generic;
using __Project.Scripts.NewSystem.Database;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.DataManager.Providers
{
    public class EnemyProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=orange>[{nameof(EnemyProvider)}]</color>";
#else
        protected string _nameLog = $"[{nameof(EnemyProvider)}]";
#endif
        private DBEnemiesFish _dbEnemiesFish { get; set; }
        
        public EnemyProvider()
        {
            TDebug.Log($"{_nameLog}: Initializing EnemyProvider...");
            _dbEnemiesFish = Resources.Load<DBEnemiesFish>("EnemiesFish");
            if (_dbEnemiesFish == null)
            {
                throw new System.Exception("Failed to load dbEnemiesFish from Resources.");
            }
        }

        public List<EnemyBase> GetEnemies(TypeOceans ocean, int levelIndex)
        {
            var listEnemies = new List<EnemyBase>();
            listEnemies.Add(_dbEnemiesFish.GetFish(ENamesFish.Enemy_Shark,ocean));
            return listEnemies;
        }
    }
}