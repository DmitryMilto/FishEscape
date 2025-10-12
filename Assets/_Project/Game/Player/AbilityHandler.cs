using System.Collections.Generic;
using _Project.Data;
using _Project.Data.Abilities;
using _Project.Data.Fish;
using UnityEngine;

namespace _Project.Game.Player
{
    public class AbilityHandler : MonoBehaviour
    {
        private List<PassiveAbilityData> passiveAbilities = new();
        private List<ActiveAbilityData> activeAbilities = new();

        public void Init(FishData fishData)
        {
            passiveAbilities = new List<PassiveAbilityData>(fishData.passiveAbilities);
            activeAbilities = new List<ActiveAbilityData>(fishData.activeAbilities);
        }

        public void UseActiveAbility(int index)
        {
            if (index < 0 || index >= activeAbilities.Count)
                return;

            ActiveAbilityData ability = activeAbilities[index];
            // TODO: выполнить активное умение
            Debug.Log($"Активное умение использовано: {ability.displayName}");
        }

        public IEnumerable<PassiveAbilityData> GetPassives() => passiveAbilities;
    }
}