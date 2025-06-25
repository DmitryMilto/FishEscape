using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Views.Gameplay
{
    public class ViewGameplay : ViewBase
    {
        [SerializeField] private Button _pauseButton;

        protected override void Awake()
        {
            base.Awake();
            _pauseButton.AddListener(OpenPausePopup);
        }

        private void OpenPausePopup()
        {
            TDebug.Log($"{_nameLog} : Opening pause popup");
            GlobalEventsManager.PauseGame(true);
            _manager.OpenViewAsync<PopupPauseGame>().Forget();
        }
    }
}