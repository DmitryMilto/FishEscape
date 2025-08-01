using __Project.Scripts.NewSystem.Fishes.Enemies;

namespace __Project.Scripts.NewSystem.Interfaces.Enemies
{
    public interface IEffectEnemy : IPausable
    {
        void ApplyEffect(EnemyBase enemy);
        void RemoveEffect(EnemyBase enemy);
    }
}