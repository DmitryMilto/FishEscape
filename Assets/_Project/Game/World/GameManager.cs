using UnityEngine;
using VContainer;
using _Project.Game.Boosts;
using _Project.Game.Enemies;
using _Project.Game.Player;
using _Project.UI.Panels;

namespace _Project.Game.World
{
    public class GameManager : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        [SerializeField] private PlayerController player;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private LevelManager levelManager;

        private EndLevelPanel _endLevelPanel;

        [Inject]
        public void Construct(EndLevelPanel endLevelPanel)
        {
            _stateMachine = new GameStateMachine();
            _endLevelPanel = endLevelPanel;
        }

        private void Start()
        {
            _stateMachine.EnterState(GameState.Intro);
            Invoke(nameof(StartGame), 2f);
        }

        public void StartGame()
        {
            _stateMachine.EnterState(GameState.Play);

            player.Initialize(); // условный метод — добавь в PlayerController
            enemySpawner.BeginSpawning();
            boostManager.BeginSpawning();
            levelManager.StartLevel();

            levelManager.OnLevelCompleted += EndGame;
        }

        public void PauseGame()
        {
            _stateMachine.EnterState(GameState.Pause);
        }

        public void EndGame()
        {
            _stateMachine.EnterState(GameState.End);

            enemySpawner.StopSpawning();
            boostManager.StopSpawning();
            levelManager.StopLevel();
            player.Disable(); // условный метод — сделай в PlayerController

            _endLevelPanel.Show();
        }
    }
}