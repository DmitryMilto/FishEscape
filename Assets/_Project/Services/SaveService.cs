using UnityEngine;
using System.IO;
using _Project.Core.Utils;

namespace _Project.Services
{
    public class SaveService
    {
        private const string SaveKey = "GameSave";

        public void Save(string json)
        {
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
            TDebug.Log("[SaveService] Game saved.");
        }

        public string Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                TDebug.LogWarning("[SaveService] No save found.");
                return null;
            }

            var json = PlayerPrefs.GetString(SaveKey);
            TDebug.Log("[SaveService] Save loaded.");
            return json;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(SaveKey);
            TDebug.Log("[SaveService] Save cleared.");
        }
    }
}