using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.Enums;
using JetBrains.Annotations;

namespace __Project.Scripts.NewSystem.DataManager.Providers
{
    [UsedImplicitly]
    public class LevelsProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=orange>[{nameof(LevelsProvider)}]</color>";
#else
        protected string _nameLog = $"[{nameof(LevelsProvider)}]";
#endif
        private readonly PlayersDataFish _player;
        private readonly EnemyDataFish _enemy;
        private BoosterDataFish _booster;

        public LevelsProvider(PlayersDataFish player, EnemyDataFish enemy, BoosterDataFish booster)
        {
            TDebug.Log($"{_nameLog}: Initializing LevelsProvider...");
            _player = player;
            _enemy = enemy;
            _booster = booster;
        }

        public LevelData Level(int levelIndex, TypeOceans ocean, ENamesFish nameFish)
        {
            TDebug.Log($"{_nameLog}: Level {levelIndex}");

            var level = new LevelData();
            level.level = levelIndex;
            level.ocean = ocean;
            level.Player = _player.GetObject(ocean);
            level.Enemies = _enemy.GetAllObjectsByOcean(ocean);
            level.Boosters = _booster.GetAllObjectsByOcean(ocean);

            if (level.Player == null || level.Enemies == null || level.Enemies.Count == 0)
            {
                TDebug.Log($"{_nameLog}: Level {levelIndex} has no player or enemies.");
                return null;
            }

            TDebug.Log(
                $"{_nameLog}: Level {levelIndex} initialized with player {level.Player.Name} and {level.Enemies.Count} enemies.");
            return level;
        }
    }
}