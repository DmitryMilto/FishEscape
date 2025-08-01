using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Enemies.Effects;
using __Project.Scripts.NewSystem.Fishes.Enemies.Moves;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Types
{
    public class EnemyShark: EnemyBase
    {
        public override ENamesFish Name => ENamesFish.Enemy_Shark;
        protected override void Awake()
        {
            // Пример: добавить нужные поведения вручную или через фабрику
            _moveBehaviours.Add(new MoveStraight());
        }
    }
}