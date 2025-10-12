using _Project.Data.Fish;
using UnityEngine;

namespace _Project.Data.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy", order = 1)]
    public class EnemyData : ScriptableObject
    {
        public string enemyId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public Sprite inGameSprite;

        [Header("Stats")] public int damage;
        public float speed;
        public float size; // радиус коллизии
        public EnemyMovementType movementType;

        [Header("Spawn")] public int minLevelToAppear;
    }
}