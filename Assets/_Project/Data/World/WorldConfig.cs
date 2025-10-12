using UnityEngine;
using System.Collections.Generic;
using _Project.Data.Enemies;
using _Project.Data.Fish;

namespace _Project.Data.World
{
    [CreateAssetMenu(fileName = "WorldConfig", menuName = "Data/WorldConfig", order = 5)]
    public class WorldConfig : ScriptableObject
    {
        public string worldId;
        public string displayName;
        public Sprite background;

        public FishData mainCharacter;
        public List<FishData> companions;
        public List<EnemyData> availableEnemies;

        [Header("Spawn Settings")]
        public float minSpawnInterval = 1f;
        public float maxSpawnInterval = 3f;

        [Header("Difficulty")]
        public int startingDifficulty;
        public float difficultyRampRate;
    }
}