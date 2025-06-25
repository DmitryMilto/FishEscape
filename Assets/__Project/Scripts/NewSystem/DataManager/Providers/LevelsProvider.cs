using __Project.Scripts.NewSystem.DataManager.Levels;
using __Project.Scripts.NewSystem.Enums;

namespace __Project.Scripts.NewSystem.DataManager.Providers
{
    public class LevelsProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=orange>[{nameof(LevelsProvider)}]</color>";
#else
        protected string _nameLog = $"[{nameof(LevelsProvider)}]";
#endif
        private readonly PlayerProvider _player;
        private readonly EnemyProvider _enemy;
        
        public LevelsProvider(PlayerProvider player, EnemyProvider enemy)
        {
            TDebug.Log($"{_nameLog}: Initializing LevelsProvider...");
            _player = player;
            _enemy = enemy;
        }

        public LevelData Level(int levelIndex, TypeOceans ocean, ENamesFish nameFish)
        {
            TDebug.Log($"{_nameLog}: Level {levelIndex}");
            if (levelIndex < 1)
            {
                TDebug.Log($"{_nameLog}: Level {levelIndex} is less than 1.");
                return null;
            }
            
            var level = new LevelData();
            level.level = levelIndex;
            level.ocean = ocean;
            level.Player = _player.GetPlayer(ocean, nameFish);
            level.Enemies = _enemy.GetEnemies(ocean, levelIndex);
            
            if(level.Player == null || level.Enemies == null || level.Enemies.Count == 0)
            {
                TDebug.Log($"{_nameLog}: Level {levelIndex} has no player or enemies.");
                return null;
            }
            
            TDebug.Log($"{_nameLog}: Level {levelIndex} initialized with player {level.Player.Name} and {level.Enemies.Count} enemies.");
            return level;
        }
    }
}