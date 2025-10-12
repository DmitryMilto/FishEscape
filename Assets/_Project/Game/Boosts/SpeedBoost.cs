using _Project.Data;
using _Project.Game.Player;
using UnityEngine;

namespace _Project.Game.Boosts
{
    [CreateAssetMenu(menuName = "Boosts/Speed")]
    public class SpeedBoost : ScriptableObject, IBoostEffect
    {
        public float speedBonus = 2f;
        public void Apply(FishStats stats) => stats.AddSpeedModifier(speedBonus);
    }

    [CreateAssetMenu(menuName = "Boosts/Heal")]
    public class HealBoost : ScriptableObject, IBoostEffect
    {
        public int healAmount = 1;
        public void Apply(FishStats stats) => stats.Heal(healAmount);
    }

    [CreateAssetMenu(menuName = "Boosts/Immortal")]
    public class ImmortalBoost : ScriptableObject, IBoostEffect
    {
        public void Apply(FishStats stats)
        {
            // Предполагается флаг или эффект: Immortal на время
        }
    }
}