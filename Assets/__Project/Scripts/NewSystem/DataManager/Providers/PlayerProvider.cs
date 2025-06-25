using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.DataManager.Providers
{
    public class PlayerProvider
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=orange>[{nameof(PlayerProvider)}]</color>";
#else
        protected string _nameLog = $"[{nameof(PlayerProvider)}]";
#endif
        private DBPlayersFish _dbPlayersFish { get; set; }
        
        public PlayerProvider()
        {
            TDebug.Log($"{_nameLog}: Initializing PlayerProvider...");
            _dbPlayersFish = Resources.Load<DBPlayersFish>("PlayersFish");
            if (_dbPlayersFish == null)
            {
                throw new System.Exception("Failed to load dbPlayersFish from Resources.");
            }
        }

        public PlayerFishBase GetPlayer(TypeOceans ocean, ENamesFish name) => _dbPlayersFish.GetFish(name, ocean);
    }
}