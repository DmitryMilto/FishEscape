using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Views.Gameplay
{
    public class ViewGameOver : ViewBase
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _comeBackHomeButton;
        
        protected override void Awake()
        {
            base.Awake();
            _restartButton.AddListener(RestartGame);
            _comeBackHomeButton.AddListener(ComeBackHome);
        }

        private void RestartGame()
        {
            TDebug.Log($"{_nameLog} : Restarting game");
            _manager.OpenViewAsync<ViewGameplay>().Forget();
            GlobalEventsManager.Replay();
            
        }
        private async void ComeBackHome()
        {
            TDebug.Log($"{_nameLog} : Coming back to home");
            await GameManager.StopGame();
        }
    }
}