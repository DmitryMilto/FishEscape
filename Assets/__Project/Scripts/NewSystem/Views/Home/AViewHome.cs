using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Home
{
    public class AViewHome: AViewBase
    {
        [Inject] private GameManager GameManager;
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
            TDebug.Log($"{LogPrefix} Open Shop");
            Manager.OpenViewAsync<PopupShop>().Forget();
        }
        private void OpenSettings()
        {
            TDebug.Log($"{LogPrefix} Open Settings");
            Manager.OpenViewAsync<PopupSetting>().Forget();
        }
        private void OpenAvatars()
        {
            TDebug.Log($"{LogPrefix} Open Avatars");
        }
        private void OpenLibrary()
        {
            TDebug.Log($"{LogPrefix} Open Library");
            Manager.OpenViewAsync<PopupBook>().Forget();
        }
        private void OpenCompany()
        {
            TDebug.Log($"{LogPrefix} Open Company");
            GameManager.StartGame(1, TypeOceans.IndianOcean).Forget();
        }
        private void OpenFree()
        {
            TDebug.Log($"{LogPrefix} Open Free");
        }
    }
}