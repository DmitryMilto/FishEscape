using Scripts.Save;
using UnityEngine;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Player", fileName = "Player", order = 1)]
    public class PlayerFish : Fish
    {
        [Range(1, 5)]
        public int Health => GameData.StartHealth;

        public int MaxHealth => GameData.MaxHealth;

        private PlayerSaveData _saveData;

        public int PuzzleCount
        {
            get => _saveData?.TargetPuzzle ?? 0;
            set
            {
                if (_saveData == null) return;
                if (value < MaxHealth)
                    _saveData.TargetPuzzle = value;
                else if (StatusCard == EnumStatusCard.Close)
                    StatusCard = EnumStatusCard.PreClose;
            }
        }

        public override EnumStatusCard StatusCard
        {
            get => _saveData?.StatusCard ?? EnumStatusCard.None;
            set
            {
                if (_saveData != null)
                    _saveData.StatusCard = value;
            }
        }

        public override void LoadData()
        {
            _saveData = SaveService.Instance.LoadPlayerData(Key) ?? new PlayerSaveData
            {
                TargetPuzzle = 0,
                StatusCard = EnumStatusCard.Close,
            };
        }

        public override void SaveData()
        {
            if (_saveData != null)
                SaveService.Instance.SavePlayerData(Key, _saveData);
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
