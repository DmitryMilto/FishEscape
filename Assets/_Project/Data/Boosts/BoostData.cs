using _Project.Game.Boosts;
using UnityEngine;

namespace _Project.Data.Boosts
{
    [CreateAssetMenu(fileName = "BoostData", menuName = "Data/Boost", order = 2)]
    public class BoostData : ScriptableObject
    {
        [Header("Meta")] public string boostId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;

        [Header("Legacy Effect Data")] public BoostType type;
        public float duration;
        public float effectStrength;

        [Header("New Effect Object")]
        public ScriptableObject effectObject; // Optional — должен реализовывать IBoostEffect

        public IBoostEffect GetEffect()
        {
            if (effectObject is IBoostEffect effect)
                return effect;

            // Legacy fallback
            return BoostEffectFactory.CreateFromLegacy(this);
        }


        public enum BoostType
        {
            SpeedUp,
            SlowDown,
            ExtraLife,
            Immortality
        }
    }
}