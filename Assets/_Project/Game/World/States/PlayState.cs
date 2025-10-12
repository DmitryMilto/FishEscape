using _Project.Core.Utils;

namespace _Project.Game.World.States
{
    public class PlayState : GameStateBase
    {
        public override void Enter()
        {
            TDebug.Log("[GameState] Entered: Play");
            // Начало геймплея, запуск спавнеров, включение управления
        }

        public override void Exit()
        {
            TDebug.Log("[GameState] Exited: Play");
        }
    }
}