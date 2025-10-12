using UnityEngine;
using System.Collections.Generic;
using _Project.Core.Utils;

namespace _Project.Services
{
    public class LocalizationService
    {
        private Dictionary<string, string> _localizedTexts;
        private string _currentLanguage = "en";

        public void LoadLanguage(string lang)
        {
            _currentLanguage = lang;
            // TODO: заменить на загрузку из файла / json
            _localizedTexts = new Dictionary<string, string>
            {
                { "play", lang == "ru" ? "Играть" : "Play" },
                { "settings", lang == "ru" ? "Настройки" : "Settings" },
                { "exit", lang == "ru" ? "Выход" : "Exit" }
            };

            TDebug.Log($"[LocalizationService] Language set to: {_currentLanguage}");
        }

        public string Get(string key)
        {
            if (_localizedTexts != null && _localizedTexts.TryGetValue(key, out var value))
                return value;

            TDebug.LogWarning($"[LocalizationService] Missing key: {key}");
            return $"#{key}";
        }
    }
}