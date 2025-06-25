using System.Collections.Generic;
using __Project.Scripts.NewSystem.Boosters;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using __Project.Scripts.NewSystem.Fishes.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.DataManager.Levels
{
    public class LevelData
    {
        public int level;
        public TypeOceans ocean;
        
        public PlayerFishBase Player;
        public List<EnemyBase> Enemies;
        public List<BoosterBase> Boosters;
        public GameObject Background;
    }
}