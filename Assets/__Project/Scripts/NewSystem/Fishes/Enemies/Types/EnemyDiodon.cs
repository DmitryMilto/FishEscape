using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Enemies.Moves;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Types
{
    public class EnemyDiodon: EnemyBase
    {
        public override ENamesFish Name => ENamesFish.Enemy_Diodon;
        protected override void Awake()
        {
            // Пример: добавить нужные поведения вручную или через фабрику
            _moveBehaviours.Add(new MoveSin());
        }
    }
}