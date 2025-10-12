using _Project.Data.Shared;
using UnityEngine;

namespace _Project.Data.Abilities
{
    [CreateAssetMenu(fileName = "PassiveAbility", menuName = "Data/Abilities/Passive", order = 3)]
    public class PassiveAbilityData : ScriptableObject
    {
        public string abilityId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public AbilityType type;
        public int unlockLevel;
        public float effectValue;
    }
}