using System;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Home;
using __Project.Scripts.NewSystem.Views.Managers;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace __Project.Scripts.NewSystem.Services
{
    [UsedImplicitly]
    public class HomeDataService : IDisposable
    {
        private readonly ViewManager _viewManager;
        private readonly BookDataService _bookDataService;

        public event Action<int, TypeOceans> OnPlayGame;

        public HomeDataService(ViewManager viewManager, BookDataService bookDataService)
        {
            _viewManager = viewManager;
            _bookDataService = bookDataService;
        }

        public void Dispose()
        {
            OnPlayGame = null;
        }

        public void OpenHome()
        {
            var view = _viewManager.GetView<ViewHome>();
            Initialize(view);
            _viewManager.OpenView(view);
        }

        private void Initialize(ViewHome view)
        {
            view.ButtonLibrary.AddListener(OpenBooksLibrary);
            view.ButtonShop?.AddListener(OpenShop);
            view.ButtonSettings?.AddListener(OpenSettings);
            view.ButtonAvatars?.AddListener(OpenAvatars);
            view.ButtonCompany?.AddListener(OpenCompany);
            view.ButtonFree?.AddListener(OpenFree);
            view.OnViewClosed += HandleCloseView;
            return;

            void HandleCloseView()
            {
                view.ButtonLibrary.RemoveListener(OpenBooksLibrary);
                view.ButtonShop?.RemoveListener(OpenShop);
                view.ButtonSettings?.RemoveListener(OpenSettings);
                view.ButtonAvatars?.RemoveListener(OpenAvatars);
                view.ButtonCompany?.RemoveListener(OpenCompany);
                view.ButtonFree?.RemoveListener(OpenFree);
                view.OnViewClosed -= HandleCloseView;
            }
        }

        private void OpenBooksLibrary()
        {
            TDebug.Log("Open Books Library");
            _bookDataService.OpenBook();
        }

        private void OpenShop()
        {
        }

        private void OpenSettings()
        {
        }

        private void OpenAvatars()
        {
        }

        private void OpenCompany()
        {
            OnPlayGame?.Invoke(1, TypeOceans.IndianOcean);
        }

        private void OpenFree()
        {
        }
    }
}