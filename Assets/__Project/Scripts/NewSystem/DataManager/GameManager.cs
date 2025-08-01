using __Project.Scripts.NewSystem.Controllers.Audios;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Views.Gameplay;
using __Project.Scripts.NewSystem.Views.Home;
using __Project.Scripts.NewSystem.Views.Managers;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.Controllers.DataManager
{
    public class GameManager : IInitializable
    {
        #if UNITY_EDITOR && ALL_DEBUG
        private static string _nameLog = $"<color=magenta>[{nameof(GameManager)}]</color>";
        #else
        private static string _nameLog = $"[{nameof(GameManager)}]";
        #endif

        public static AudioController Audio {get; private set; }
        public static ViewManager ViewManager{ get; private set; }
        public static LevelData LevelData { get; private set; }

        [Preserve]
        public GameManager(ViewManager viewManager, AudioController audioController)
        {
            Audio = audioController;
            ViewManager = viewManager;
            TDebug.Log($"{_nameLog}: Creating GameManager...");
        }
        public void Initialize()
        {
            TDebug.Log($"{_nameLog}: Initializing GameManager...");
            // Additional initialization logic can be added here if needed
        }
        
        public static async UniTask<bool> StartGame(int index, TypeOceans ocean)
        {
            await ViewManager.OpenViewAsync<ViewSplash>();
            if (index <= 0)
            {
                TDebug.Log($"{_nameLog}: Invalid fish index {index}. Cannot start game.");
                return false;
            }

            if (ocean == TypeOceans.None)
            {
                TDebug.Log($"{_nameLog}: Invalid ocean type {ocean}. Cannot start game.");
                return false;
            }
            TDebug.Log($"{_nameLog}: Starting game with fish index {index} in ocean {ocean}");
            LevelData = AppData.GetLevel(index, ocean);
            if (LevelData == null)
            {
                TDebug.Log($"{_nameLog}: Invalid fish index {index}. Cannot start game.");
                return false;
            }
            await SceneManager.LoadSceneAsync("Run");
            Audio.Play(SoundType.BackgroundGame);
            await ViewManager.OpenViewAsync<ViewGameplay>();
            return true;
        }

        public static async UniTask StopGame()
        {
            await ViewManager.OpenViewAsync<ViewSplash>();
            if (LevelData == null)
            {
                TDebug.LogError($"{_nameLog}: LevelData is null. Cannot stop game.");
                return;
            }
            await SceneManager.LoadSceneAsync("Home");
            Audio.Play(SoundType.BackgroundMenu);
            await ViewManager.OpenViewAsync<ViewHome>();
        }
    }
}