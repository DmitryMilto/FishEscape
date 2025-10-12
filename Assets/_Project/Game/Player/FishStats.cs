using _Project.Data.Fish;

namespace _Project.Game.Player
{
    public class FishStats
    {
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public float BaseSpeed { get; private set; }
        private float _speedModifier;

        public float Speed => BaseSpeed + _speedModifier;
        public int Lives { get; set; }

        private FishData _data;

        public FishStats(FishData data)
        {
            _data = data;
            Health = data.startHealth;
            MaxHealth = data.maxHealth;
            BaseSpeed = data.baseSpeed;
        }

        public void Heal(int amount)
        {
            Health = System.Math.Min(Health + amount, MaxHealth);
        }

        public void TakeDamage(int damage)
        {
            Health = System.Math.Max(Health - damage, 0);
        }

        public bool IsDead() => Health <= 0;

        public void AddSpeedModifier(float value)
        {
            _speedModifier += value;
        }

        public void RemoveSpeedModifier(float value)
        {
            _speedModifier -= value;
        }

        public void ResetStats()
        {
            throw new System.NotImplementedException();
        }
    }
}