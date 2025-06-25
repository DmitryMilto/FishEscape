using System;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Levels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses
{
    public class GameRunningManager : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog => $"<color=#0011AE>[{nameof(GameRunningManager)}]</color>";
#else
        protected string _nameLog => $"[{nameof(GameRunningManager)}]";
#endif
        
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemiesSpawnPoint;
        [SerializeField] private Transform _backgroundSpawnPoint;

        public FishProvider Player {get; private set;}
        public EnemiesProvider Enemies { get; private set; }
        public BackgroundProvider Background { get; private set; }
        
        private LevelData _levelData;
        
        private void Awake()
        {
            _levelData = GameManager.LevelData;
            if (_levelData == null)
            {
                TDebug.LogError($"{_nameLog} LevelData is null. Cannot initialize GameRunningManager.");
                GameManager.StopGame().Forget();
                return;
            }
            Player ??= new FishProvider(_playerSpawnPoint, _levelData.Player);
            Enemies ??= new EnemiesProvider(_enemiesSpawnPoint, _levelData.Enemies, _levelData.Boosters);
            Background ??= new BackgroundProvider(_backgroundSpawnPoint, _levelData.Background);
            
            GlobalEventsManager.OnDeath += GameOverGame;
            GlobalEventsManager.OnReplay += ReplayGame;
            GlobalEventsManager.OnPause += Pause;
        }

        private void GameOverGame()
        {
            TDebug.Log($"{_nameLog} Game Over!");
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
            Player?.Update();
            Enemies?.Update();
            Background?.Update();
        }

        private void OnDestroy()
        {
            TDebug.Log($"{_nameLog} OnDestroy");
            Player?.DestroyProvider();
            Enemies?.DestroyProvider();
            Background?.DestroyProvider();
        }
    }
}