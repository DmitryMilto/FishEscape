using UnityEngine;
using System.Collections.Generic;
using _Project.Data.Abilities;

namespace _Project.Data.Fish
{
    [CreateAssetMenu(fileName = "FishData", menuName = "Data/Fish", order = 0)]
    public class FishData : ScriptableObject
    {
        public string fishId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public Sprite inGameSprite;

        [Header("Stats")] public int startHealth;
        public int maxHealth;
        public float baseSpeed;

        [Header("Progression")] public int unlockLevel;

        [Header("Abilities")] public List<PassiveAbilityData> passiveAbilities;
        public List<ActiveAbilityData> activeAbilities;
    }
    
    public enum EnemyMovementType
    {
        Straight,
        Zigzag,
        Dash,
        Homing
    }
}