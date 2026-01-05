using UnityEngine;

namespace Scripts.Save
{
    public class SaveService
    {
        private static SaveService _instance;
        public static SaveService Instance => _instance ??= new SaveService();

        public PlayerSaveData LoadPlayerData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var data = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<PlayerSaveData>(data);
            }
            return null;
        }

        public void SavePlayerData(string key, PlayerSaveData data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }

        public EnemySaveData LoadEnemyData(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var data = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<EnemySaveData>(data);
            }
            return null;
        }

        public void SaveEnemyData(string key, EnemySaveData data)
        {
            var json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, json);
        }
    }
}

