using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.Views.Gameplay;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses
{
    public class GameRunningManager : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog => $"<color=#0011AE>[{nameof(GameRunningManager)}]</color>";
#else
        protected string _nameLog => $"[{nameof(GameRunningManager)}]";
#endif
        [Inject] public GameManager GameManager { get; set; }

        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemiesSpawnPoint;
        [SerializeField] private Transform _backgroundSpawnPoint;

        public PlayerFishManager Player { get; private set; }
        public EnemyManager Enemies { get; private set; }
        public BackgroundProvider Background { get; private set; }

        private LevelData _levelData;

        private void Start()
        {
            _levelData = GameManager.LevelData;
            if (_levelData == null)
            {
                TDebug.LogError($"{_nameLog} LevelData is null. Cannot initialize GameRunningManager.");
                GameManager.StopGame().Forget();
                return;
            }

            Player ??= new PlayerFishManager(_playerSpawnPoint, _levelData.Player);
            Enemies ??= new EnemyManager(_enemiesSpawnPoint, Player, _levelData.Enemies, _levelData.Boosters);
            Background ??= new BackgroundProvider(_backgroundSpawnPoint, _levelData.Background);

            GlobalEventsManager.OnDeath += GameOverGame;
            GlobalEventsManager.OnReplay += ReplayGame;
            GlobalEventsManager.OnPause += Pause;
        }

        private void GameOverGame()
        {
            TDebug.Log($"{_nameLog} Game Over!");
            GameManager.ViewManager.OpenViewAsync<AViewGameOver>().Forget();
            Player?.GameOverGame();
            Enemies?.GameOverGame();
            Background?.GameOverGame();
        }

        private void Pause(bool paused)
        {
            TDebug.Log($"{_nameLog} PauseGame called with paused={paused}");
            Player?.PauseGame(paused);
            Enemies?.PauseGame(paused);
            Background?.PauseGame(paused);
        }

        private void ReplayGame()
        {
            TDebug.Log($"{_nameLog} ResumeGame called");
            Player?.ResumeGame();
            Enemies?.ResumeGame();
            Background?.ResumeGame();
        }

        private void Update()
        {
            Enemies?.Update();
            Background?.Update();
            Player?.Update();
        }

        private void OnDestroy()
        {
            TDebug.Log($"{_nameLog} OnDestroy");
            GlobalEventsManager.OnDeath -= GameOverGame;
            GlobalEventsManager.OnReplay -= ReplayGame;
            GlobalEventsManager.OnPause -= Pause;
            Player?.DestroyProvider();
            Enemies?.DestroyProvider();
            Background?.DestroyProvider();
            // Для предотвращения утечек памяти
        }
    }
}