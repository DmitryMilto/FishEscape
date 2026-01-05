using UnityEngine;
using FishEscape.Enums.Players;
using System.Collections.Generic;

namespace FishEscape.Fishs
{
    // Данные для игровой части
    [System.Serializable]
    public class FishGameData
    {
        public float Speed;
        public int StartHealth;
        public int MaxHealth;
        public GameObject Prefab;
        public List<EnumOcean> Habitats = new List<EnumOcean>();
        public float MinSpeed;
        public float MaxSpeed;
    }

    // Данные для книги (словаря)
    [System.Serializable]
    public class FishBookData
    {
        public Sprite Illustration;
        [TextArea]
        public string Description;
        public Sprite RealPhoto;
    }

    public abstract class Fish : ScriptableObject
    {
        public Sprite FishSprite;

        public string FishName;

        [SerializeField]
        private TypeFish type;
        
        public FishGameData GameData = new();
        
        public FishBookData BookData = new();

        [Space]
        public string Key;

        #region Abstract Method
        public abstract EnumStatusCard StatusCard { get; set; }
        public abstract void LoadData();
        public abstract void SaveData();
        public abstract void Update();
        #endregion

#if UNITY_EDITOR
        protected void Save()
        {
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        private void DeleteKeyInformation()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                Debug.Log($"<color='yellow'>Удаление данных {this.Key}...</color>");
                PlayerPrefs.DeleteKey(Key);
                if (!PlayerPrefs.HasKey(Key)) Debug.Log($"<color='green'>Данные {this.Key} успешно удалены!</color>");
                else Debug.Log($"<color='red'>Данные {this.Key} не были удалены!</color>");
            }
            else
            {
                Debug.Log($"<color='red'>Данные {this.Key} не найдены!</color>");
            }
        }
#endif
    }
}