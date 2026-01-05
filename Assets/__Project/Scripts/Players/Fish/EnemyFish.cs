using UnityEngine;
using Scripts.Save;
using System.Collections.Generic;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Enemy", fileName = "Enemy", order = 1)]
    public class EnemyFish : Fish
    {
        [Range(1, 4)]
        public int Damage = 1;

        public List<EnumOcean> Habitats => GameData.Habitats;

        public float MinSpeed => GameData.MinSpeed;

        public float MaxSpeed => GameData.MaxSpeed;

        private EnemySaveData _saveData;

        public override EnumStatusCard StatusCard
        {
            get
            {
                if (_saveData == null) return EnumStatusCard.None;
                if (_saveData.StatusCard == EnumStatusCard.Close || _saveData.StatusCard == EnumStatusCard.None || _saveData.StatusCard == EnumStatusCard.PreOpen)
                    _saveData.StatusCard = EnumStatusCard.PreClose;
                return _saveData.StatusCard;
            }
            set
            {
                if (_saveData != null)
                    _saveData.StatusCard = value;
            }
        }

        public override void LoadData()
        {
            _saveData = SaveService.Instance.LoadEnemyData(Key) ?? new EnemySaveData
            {
                StatusCard = EnumStatusCard.Close,
            };
        }

        public override void SaveData()
        {
            if (_saveData != null)
                SaveService.Instance.SaveEnemyData(Key, _saveData);
        }

        public override void Update()
        {
            if (FishSprite != null)
            {
                FishName = FishSprite.name;
                Key = FishName.Trim();
            }
        }
    }
}
