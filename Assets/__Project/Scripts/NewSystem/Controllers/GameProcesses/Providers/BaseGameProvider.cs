using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses.Providers
{
    public abstract class BaseGameProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog => $"<color=#0011AE>[{this.GetType().Name}]</color>";
#else
        protected string _nameLog => $"[{this.GetType().Name}]";
#endif
        protected readonly Transform _spawnPoint;
        protected bool isPauseGame;
        protected bool isGameOver;
        
        protected BaseGameProvider(Transform spawnPoint)
        {
            TDebug.Log($"{_nameLog}: Creating new game provider.");
            _spawnPoint = spawnPoint;
        }

        public virtual void PauseGame(bool isPause)
        {
            TDebug.Log($"{_nameLog}: Pause game - {isPause}");
            isPauseGame = isPause;
        }

        public abstract void NewGame();
        public abstract void GameOverGame();
        public abstract void ResumeGame();
        public abstract void Update();
        public abstract void DestroyProvider();
    }
}