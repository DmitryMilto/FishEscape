using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.DataManager.Providers;
using __Project.Scripts.NewSystem.Enums;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.DataManager
{
    public class AppData : IInitializable
    {
#if UNITY_EDITOR
        private static string _nameLog = $"<color=cyan>[{nameof(AppData)}]</color>";
#else
        private static string _nameLog = $"[{nameof(AppData)}]";
#endif
        public static PlayerProvider User { get;private set; }
        public static EnemyProvider Enemy { get; private set; }
        public static LevelsProvider Levels { get; private set; }
        
        [Preserve]
        public AppData()
        {
            TDebug.Log($"{_nameLog}: Creating AppData...");
        }

        public void Initialize()
        {
            TDebug.Log($"{_nameLog}: Initializing AppData...");
            User ??= new PlayerProvider();
            Enemy ??= new EnemyProvider();
            Levels ??= new LevelsProvider(User, Enemy);
        }
        

        public static LevelData GetLevel(int index, TypeOceans ocean)
        {
            var level = Levels.Level(index, ocean, ENamesFish.Player_Salmon);
            return level;
        }
    }
}