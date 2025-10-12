using _Project.Core.Utils;
using UnityEngine;

namespace _Project.Game.World.States
{
    public class PauseState : GameStateBase
    {
        public override void Enter()
        {
            TDebug.Log("[GameState] Entered: Pause");
            Time.timeScale = 0f;
        }

        public override void Exit()
        {
            TDebug.Log("[GameState] Exited: Pause");
            Time.timeScale = 1f;
        }
    }
}