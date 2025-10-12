using _Project.Core.Utils;

namespace _Project.Game.World.States
{
    public class IntroState : GameStateBase
    {
        public override void Enter()
        {
            TDebug.Log("[GameState] Entered: Intro");
            // Подготовка к игре (заставка, обучение и т.п.)
        }

        public override void Exit()
        {
            TDebug.Log("[GameState] Exited: Intro");
        }
    }
}