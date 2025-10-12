using _Project.Data.Shared;
using UnityEngine;

namespace _Project.Data.Abilities
{
    [CreateAssetMenu(fileName = "ActiveAbility", menuName = "Data/Abilities/Active", order = 4)]
    public class ActiveAbilityData : ScriptableObject
    {
        public string abilityId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public AbilityType type;
        public int unlockLevel;
        public float cooldown;
        public float effectValue;
    }
}