using Scripts.Save;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Player", fileName = "Player", order = 1)]
    public class PlayerFish : Fish
    {
        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(1, 5)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public int health = 1;

        [Space]
        [SerializeField]
        public int MaxPazzle;

        public int Pazzle
        {
            get
            {
                return saveData.TargetPuzzle;
            }
            set
            {
                if (value < MaxPazzle)
                    saveData.TargetPuzzle = value;
                else
                {
                    if (StatusCard == EnumStatusCard.Close)
                        StatusCard = EnumStatusCard.PreClose;
                }
            }
        }
        public override EnumStatusCard StatusCard
        {
            get
            {
                if (saveData.TargetPuzzle == MaxPazzle)
                {
                    if (saveData.StatusCard == EnumStatusCard.Close)
                        saveData.StatusCard = EnumStatusCard.PreClose;
                }
                else
                {
                    if (saveData.StatusCard == EnumStatusCard.PreOpen)
                        saveData.StatusCard = EnumStatusCard.PreClose;
                }
                return saveData.StatusCard;
            }
            set
            {
                saveData.StatusCard = value;
            }
        }

        private PlayerSaveData _saveData;
        private PlayerSaveData saveData
        {
            get
            {
                if(_saveData == null)
                {
                    Debug.Log($"{this.fishName} data null");
                    return null;
                }
                return _saveData;
            }
            set
            {
                _saveData = value;
            }
        }
        public override void LoadData()
        {
            Debug.Log($"Loading Data {this.fishName}...");
            if (PlayerPrefs.HasKey(Key))
            {
                var data = PlayerPrefs.GetString(Key);
                saveData = JsonUtility.FromJson<PlayerSaveData>(data);
            }
            else
            {
                saveData = new PlayerSaveData
                {
                    TargetPuzzle = 0,
                    StatusCard = EnumStatusCard.Close,
                };
                SaveData();
            }
        }
        public override void SaveData()
        {
            Debug.Log($"Saving Data {this.fishName}...");
            var data = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString(Key, data);
        }
        public override void Update()
        {
            if (this.fish != null)
            {
                this.fishName = this.fish.name;
                var key = this.fish.name.Trim();
                this.Key = key;
            }
        }
    }
}
