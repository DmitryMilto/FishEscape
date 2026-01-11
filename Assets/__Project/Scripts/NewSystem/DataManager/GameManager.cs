using System;
using __Project.Scripts.NewSystem.Controllers.Audios;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Services;
using __Project.Scripts.NewSystem.Views.Gameplay;
using __Project.Scripts.NewSystem.Views.Home;
using __Project.Scripts.NewSystem.Views.Managers;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.Controllers.DataManager
{
    [UsedImplicitly]
    public class GameManager : IDisposable
    {
#if UNITY_EDITOR && ALL_DEBUG
        private static string _nameLog = $"<color=magenta>[{nameof(GameManager)}]</color>";
#else
        private static string _nameLog = $"[{nameof(GameManager)}]";
#endif
        private readonly AppData _appData;

        public AudioController Audio { get; private set; }
        public ViewManager ViewManager { get; private set; }
        public LevelData LevelData { get; private set; }
        private readonly HomeRouting _homeDataService;

        public event Action OnOpenHome;

        public GameManager(AppData appData, ViewManager viewManager, AudioController audioController)
        {
            _appData = appData;
            Audio = audioController;
            ViewManager = viewManager;
            TDebug.Log($"{_nameLog}: Creating GameManager...");
        }
        
        public void Dispose()
        {
            OnOpenHome = null;
        }

        public async UniTask<bool> StartGame(int index, TypeOceans ocean)
        {
            await ViewManager.OpenViewAsync<ViewSplash>();

            if (ocean == TypeOceans.None)
            {
                TDebug.Log($"{_nameLog}: Invalid ocean type {ocean}. Cannot start game.");
                return false;
            }

            TDebug.Log($"{_nameLog}: Starting game with fish index {index} in ocean {ocean}");
            LevelData = _appData.GetLevel(index, ocean);
            if (LevelData == null)
            {
                TDebug.Log($"{_nameLog}: Invalid fish index {index}. Cannot start game.");
                return false;
            }

            await SceneManager.LoadSceneAsync("Run").ToUniTask();
            Audio.Play(SoundType.BackgroundGame);
            await ViewManager.OpenViewAsync<ViewGameplay>();
            return true;
        }

        public async UniTask StopGame()
        {
            await ViewManager.OpenViewAsync<ViewSplash>();
            if (LevelData == null)
            {
                TDebug.LogError($"{_nameLog}: LevelData is null. Cannot stop game.");
                return;
            }

            await SceneManager.LoadSceneAsync("Home").ToUniTask();
            Audio.Play(SoundType.BackgroundMenu);
            OnOpenHome?.Invoke();
        }
    }
}