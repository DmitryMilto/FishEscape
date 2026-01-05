using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using __Project.Scripts.NewSystem.Views.Home;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Gameplay
{
    public class PopupPauseGame : PopupBase
    {
        [Inject] private GameManager GameManager;
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
            TDebug.Log($"{LogPrefix} : Restarting game");
        }
        private async void ComeBackHome()
        {
            TDebug.Log($"{LogPrefix} : Coming back to home");
            Manager.CloseViewAsync(this).Forget();
            await GameManager.StopGame();
        }

        protected override void ClosePopup()
        {
            GlobalEventsManager.PauseGame(false);
            base.ClosePopup();
        }
    }
}