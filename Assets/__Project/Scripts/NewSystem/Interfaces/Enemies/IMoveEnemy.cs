using __Project.Scripts.NewSystem.Fishes.Enemies;

namespace __Project.Scripts.NewSystem.Interfaces.Enemies
{
    public interface IMoveEnemy : IPausable
    {
        void Move(EnemyBase enemy);
    }
}