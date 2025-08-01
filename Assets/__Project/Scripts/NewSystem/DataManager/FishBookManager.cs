using System.Collections.Generic;
using System.IO;
using System.Linq;
using __Project.Scripts.NewSystem.Data;
using __Project.Scripts.NewSystem.Database;
using __Project.Scripts.NewSystem.Elements;
using __Project.Scripts.NewSystem.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace __Project.Scripts.NewSystem.DataManager
{
    public class FishBookManager
    {
        private const string ProgressFileName = "fish_book_progress.json";
        private readonly IFileManager _fileManager;
        
        private FishBookDatabase _database;
        private Dictionary<string, FishCardState> _cardStates = new();
        private int _currentIndex = 0;
        private List<FishBookEntry> _currentList = new();
        
        [Inject]
        public FishBookManager(FishBookDatabase database, IFileManager fileManager)
        {
            _fileManager = fileManager;
            _database = database;
            LoadProgressAsync().Forget();
        }
        
        public List<FishBookEntry> GetFishByOceanAndType(TypeOceans ocean, FishType type)
        {
            _currentList = _database.entries
                .Where(e => e.ocean == ocean && e.fishType == type)
                .ToList();
            _currentIndex = 0;
            return _currentList;
        }

        public List<FishBookEntry> GetAllFishByType(FishType type) =>
            _database.entries.Where(e => e.fishType == type).ToList();
        public FishCardState GetCardState(string fishId) =>
            _cardStates.TryGetValue(fishId, out var state) ? state : FishCardState.Locked;
        
        public void ShowPrevFish()
        {
            if (_currentList.Count == 0) return;
            _currentIndex = (_currentIndex - 1 + _currentList.Count) % _currentList.Count;
            // Вызвать обновление UI (например, через событие)
        }

        public void ShowNextFish()
        {
            if (_currentList.Count == 0) return;
            _currentIndex = (_currentIndex + 1) % _currentList.Count;
            // Вызвать обновление UI (например, через событие)
        }

        public List<FishBookEntry> GetFishByOcean(TypeOceans ocean)
        {
            _currentList = _database.entries.Where(e => e.ocean == ocean).ToList();
            _currentIndex = 0;
            return _currentList;
        }

        public async UniTask SetCardStateAsync(string fishId, FishCardState state)
        {
            _cardStates[fishId] = state;
            await SaveProgressAsync();
        }

        private async UniTask LoadProgressAsync()
        {
            var progress = await _fileManager.LoadAsync<FishBookProgress>(ProgressFileName);
            if (progress != null && progress.CardStates != null)
            {
                _cardStates = progress.CardStates.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (FishCardState)kvp.Value
                );
            }
            // Заполняем отсутствующие карточки состоянием Locked
            foreach (var entry in _database.entries)
            {
                if (!_cardStates.ContainsKey(entry.fishId))
                {
                    _cardStates[entry.fishId] = entry.initialState == FishCardInitialState.Open
                        ? FishCardState.Open
                        : FishCardState.Locked;
                }
            }
        }

        private async UniTask SaveProgressAsync()
        {
            var progress = new FishBookProgress
            {
                CardStates = _cardStates.ToDictionary(
                    kvp => kvp.Key,
                    kvp => (int)kvp.Value
                )
            };
            await _fileManager.SaveAsync(ProgressFileName, progress);
        }

        public List<FishBookEntry> GetAllFish() => _database.entries.ToList();
    }
}