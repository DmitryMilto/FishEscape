using _Project.Core.Utils;
using _Project.Data.Boosts;
using _Project.Game.Player;
using UnityEngine;

namespace _Project.Game.Boosts
{
    public static class BoostEffectFactory
    {
        public static IBoostEffect CreateFromLegacy(BoostData data)
        {
            return data.type switch
            {
                // BoostData.BoostType.Speed => new RuntimeSpeedBoost(data.effectStrength),
                BoostData.BoostType.Immortality => new RuntimeImmortalBoost(data.duration),
                // BoostData.BoostType.Heal => new RuntimeHealBoost(),
                _ => new NoEffect(),
            };
        }
    }

    // Runtime-реализации, если не используются ScriptableObject
    public class RuntimeSpeedBoost : IBoostEffect
    {
        private readonly float _amount;

        public RuntimeSpeedBoost(float amount) => _amount = amount;

        public void Apply(FishStats stats)
        {
            stats.Speed += _amount;
            TDebug.Log($"[RuntimeSpeedBoost] Speed += {_amount}");
        }
    }

    public class RuntimeImmortalBoost : IBoostEffect
    {
        private readonly float _duration;

        public RuntimeImmortalBoost(float duration) => _duration = duration;

        public void Apply(FishStats stats)
        {
            stats.SetImmortal(_duration);
            TDebug.Log($"[RuntimeImmortalBoost] Player is immortal for {_duration}s");
        }
    }

    public class RuntimeHealBoost : IBoostEffect
    {
        public void Apply(FishStats stats)
        {
            stats.Lives = Mathf.Min(stats.MaxLives, stats.Lives + 1);
            TDebug.Log($"[RuntimeHealBoost] Healed 1 life");
        }
    }

    public class NoEffect : IBoostEffect
    {
        public void Apply(FishStats stats)
        {
            TDebug.Log("[NoEffect] No boost effect applied.");
        }
    }
}