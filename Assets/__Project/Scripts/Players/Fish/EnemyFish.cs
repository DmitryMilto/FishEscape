using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using FishEscape.Enums;
using Scripts.Save;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Enemy", fileName = "Enemy", order = 1)]
    public class EnemyFish : Fish
    {
        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(1, 4)]
        [GUIColor(0.8f, 0.4f, 0.4f)]
        public int damage = 1;

        [BoxGroup("Other Setting")]
        [Range(0f,1f)]
        public float scale = .1f;

        [BoxGroup("Other Setting")]
        public List<ListBuffer> list;
        [BoxGroup("Other Setting")]
        public EnumTypeMove typeMove;

        private EnemySaveData saveData;

        public override EnumStatusCard StatusCard
        {
            get 
            {
                if (saveData.StatusCard == EnumStatusCard.Close || saveData.StatusCard == EnumStatusCard.None || saveData.StatusCard == EnumStatusCard.PreOpen)
                    saveData.StatusCard = EnumStatusCard.PreClose;
                return saveData.StatusCard;
            }
            set
            {
                saveData.StatusCard = value;
            }
        }

        public override void LoadData()
        {
            Debug.Log($"Loading Data {this.fishName}...");
            if (PlayerPrefs.HasKey(Key))
            {
                var data = PlayerPrefs.GetString(Key);
                saveData = JsonUtility.FromJson<EnemySaveData>(data);
            }
            else
            {
                saveData = new EnemySaveData
                {
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
    }
}
