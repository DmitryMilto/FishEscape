using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.DataManager.Providers;
using __Project.Scripts.NewSystem.Enums;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.DataManager
{
    public class AppData
    {
#if UNITY_EDITOR
        private static string _nameLog = $"<color=cyan>[{nameof(AppData)}]</color>";
#else
        private static string _nameLog = $"[{nameof(AppData)}]";
#endif
        private readonly LevelsProvider _levels;

        public AppData(LevelsProvider levels)
        {
            TDebug.Log($"{_nameLog}: Initializing AppData...");
            _levels = levels;
        }

        public LevelData GetLevel(int index, TypeOceans ocean)
        {
            var level = _levels.Level(index, ocean, ENamesFish.Player_Salmon);
            return level;
        }
    }
}