using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Enums;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace __Project.Scripts.NewSystem.Services
{
    [UsedImplicitly]
    public class HomeRouting
    {
        private readonly HomeDataService _dataService;
        private readonly GameManager _gameManager;

        public HomeRouting(HomeDataService homeDataService, GameManager gameManager)
        {
            _dataService = homeDataService;
            _gameManager = gameManager;
        }

        public void OpenHome()
        {
            _gameManager.OnOpenHome -= OpenHome;
            _dataService.OnPlayGame += StartGame;
            _dataService.OpenHome();
        }

        private void StartGame(int index, TypeOceans ocean)
        {
            _dataService.OnPlayGame -= StartGame;
            _gameManager.OnOpenHome += OpenHome;
            _gameManager.StartGame(index, ocean).Forget();
        }
    }
}