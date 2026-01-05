using System.Collections.Generic;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Elements;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace __Project.Scripts.NewSystem.Views.Gameplay
{
    public class ViewGameplay : AViewBase
    {
        [Inject] private GameManager GameManager;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Transform _heartsContainer;
        [SerializeField] private LifePoint _heartPrefab;

        private readonly List<LifePoint> _heartsPool = new();
        private int _maxHearts = 5;
        private int _currentHearts;

        protected void Start()
        {
            _pauseButton.AddListener(OpenPausePopup);

            _currentHearts = GameManager.LevelData.Player.MaxLives;
            // Подписка на события изменения жизней
            GlobalEventsManager.OnAddLife += OnAddLife;
            GlobalEventsManager.OnRemoveLife += OnRemoveLife;
            GlobalEventsManager.OnReplay += ReplayGame;

            CreateLifePoints();
        }

        private void OnDestroy()
        {
            GlobalEventsManager.OnAddLife -= OnAddLife;
            GlobalEventsManager.OnRemoveLife -= OnRemoveLife;
            GlobalEventsManager.OnReplay -= ReplayGame;
        }

        private void OpenPausePopup()
        {
            TDebug.Log($"{LogPrefix} : Opening pause popup");
            GlobalEventsManager.PauseGame(true);
            Manager.OpenViewAsync<PopupPauseGame>().Forget();
        }

        private void ReplayGame()
        {
            _maxHearts = GameManager.LevelData.Player.MaxLives;
            _currentHearts = GameManager.LevelData.Player.StartLives;

            for (var i = 0; i < _heartsPool.Count; i++)
            {
                _heartsPool[i].ResetState(i < _currentHearts);
            }
        }

        private void OnAddLife()
        {
            _currentHearts++;
            UpdateHeartsUI(_currentHearts);
            TDebug.Log($"[{LogPrefix}] : Life added. Current lives: {_currentHearts}");
        }

        private void OnRemoveLife()
        {
            _currentHearts--;
            UpdateHeartsUI(_currentHearts);
            TDebug.Log($"[{LogPrefix}] : Life removed. Current lives: {_currentHearts}");
        }

        private void OnMaxLifeChanged(int maxLife)
        {
            _maxHearts = maxLife;
            UpdateHeartsUI(_currentHearts);
        }

        private void OnLifeChanged(int currentLife)
        {
            _currentHearts = currentLife;
            UpdateHeartsUI(_currentHearts);
        }

        private void CreateLifePoints()
        {
            // Создание пула жизней
            for (var i = 0; i < _maxHearts; i++)
            {
                var heart = Instantiate(_heartPrefab, _heartsContainer);
                _heartsPool.Add(heart);
                heart.Init(i, i < _currentHearts);
                heart.gameObject.SetActive(true);
            }
        }

        private void UpdateHeartsUI(int currentLife)
        {
            // Анимация добавления/отнимания
            for (var i = 0; i < _maxHearts; i++)
            {
                _heartsPool[i].SetStateAsync(currentLife, _canvas.GetCancellationTokenOnDestroy()).Forget();
            }
        }
    }
}