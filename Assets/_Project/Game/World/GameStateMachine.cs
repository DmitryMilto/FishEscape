using System;
using System.Collections.Generic;
using _Project.Game.World.States;
using _Project.Core.Utils;

namespace _Project.Game.World
{
    public class GameStateMachine
    {
        private readonly Dictionary<GameState, GameStateBase> _states;
        private GameStateBase _currentState;
        public GameState CurrentState { get; private set; }

        public GameStateMachine()
        {
            _states = new Dictionary<GameState, GameStateBase>
            {
                { GameState.Intro, new IntroState() },
                { GameState.Play, new PlayState() },
                { GameState.Pause, new PauseState() },
                { GameState.End, new EndState() },
            };
        }

        public void EnterState(GameState newState)
        {
            if (CurrentState == newState)
                return;

            _currentState?.Exit();

            CurrentState = newState;
            _currentState = _states[newState];
            _currentState.Enter();

            TDebug.Log($"[GameStateMachine] Switched to: {newState}");
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}