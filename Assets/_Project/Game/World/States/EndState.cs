using _Project.Core.Utils;

namespace _Project.Game.World.States
{
    public class EndState : GameStateBase
    {
        public override void Enter()
        {
            TDebug.Log("[GameState] Entered: End");
            // Показ финального экрана, статистика, награды
        }

        public override void Exit()
        {
            TDebug.Log("[GameState] Exited: End");
        }
    }
}