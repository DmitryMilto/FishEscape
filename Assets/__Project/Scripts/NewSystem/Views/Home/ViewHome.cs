using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Views.Home
{
    public class ViewHome: ViewBase
    {
        [SerializeField] private Button _buttonShop;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAvatars;
        [SerializeField] private Button _buttonLibrary;
        [SerializeField] private Button _buttonCompany;
        [SerializeField] private Button _buttonFree;

        protected override void Awake()
        {
            base.Awake();
            _buttonShop?.AddListener(OpenShop);
            _buttonSettings?.AddListener(OpenSettings);
            _buttonAvatars?.AddListener(OpenAvatars);
            _buttonLibrary?.AddListener(OpenLibrary);
            _buttonCompany?.AddListener(OpenCompany);
            _buttonFree?.AddListener(OpenFree);
        }

        private void OnDestroy()
        {
            _buttonShop?.RemoveListener(OpenShop);
            _buttonSettings?.RemoveListener(OpenSettings);
            _buttonAvatars?.RemoveListener(OpenAvatars);
            _buttonLibrary?.RemoveListener(OpenLibrary);
            _buttonCompany?.RemoveListener(OpenCompany);
            _buttonFree?.RemoveListener(OpenFree);
        }

        private void OpenShop()
        {
            TDebug.Log($"{_nameLog} Open Shop");
            _manager.CloseViewAsync(this).Forget();
        }
        private void OpenSettings()
        {
            TDebug.Log($"{_nameLog} Open Settings");
            _manager.OpenViewAsync<PopupSetting>().Forget();
        }
        private void OpenAvatars()
        {
            TDebug.Log($"{_nameLog} Open Avatars");
        }
        private void OpenLibrary()
        {
            TDebug.Log($"{_nameLog} Open Library");
        }
        private void OpenCompany()
        {
            TDebug.Log($"{_nameLog} Open Company");
            GameManager.StartGame(1, TypeOceans.IndianOcean).Forget();
        }
        private void OpenFree()
        {
            TDebug.Log($"{_nameLog} Open Free");
        }
    }
}