using System.Collections.Generic;
using System.Linq;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.DataManager;
        using __Project.Scripts.NewSystem.Elements;
        using __Project.Scripts.NewSystem.Tools;
        using __Project.Scripts.NewSystem.Views.Base;
        using Cysharp.Threading.Tasks;
        using DG.Tweening;
        using UnityEngine;
        using UnityEngine.UI;
        
        namespace __Project.Scripts.NewSystem.Views.Gameplay
        {
            public class ViewGameplay : ViewBase
            {
                [SerializeField] private Button _pauseButton;
                [SerializeField] private Transform _heartsContainer;
                [SerializeField] private LifePoint _heartPrefab;
        
                private readonly List<LifePoint> _heartsPool = new();
                private int _maxHearts = 5;
                private int _currentHearts;
        
                protected override void Awake()
                {
                    base.Awake();
                    _pauseButton.AddListener(OpenPausePopup);
        
                    _currentHearts = GameManager.LevelData.Player.MaxLives;
                    // Подписка на события изменения жизней
                    GlobalEventsManager.OnAddLife += OnAddLife;
                    GlobalEventsManager.OnRemoveLife += OnRemoveLife;
                    // GlobalEventsManager.OnMaxLifeChanged += OnMaxLifeChanged;
                    
                    CreaateLifePoints();
                }
        
                private void OnDestroy()
                {
                    GlobalEventsManager.OnAddLife -= OnAddLife;
                    GlobalEventsManager.OnRemoveLife -= OnRemoveLife;
                    // GlobalEventsManager.OnMaxLifeChanged -= OnMaxLifeChanged;
                }
        
                private void OpenPausePopup()
                {
                    TDebug.Log($"{_nameLog} : Opening pause popup");
                    GlobalEventsManager.PauseGame(true);
                    _manager.OpenViewAsync<PopupPauseGame>().Forget();
                }
                
                private void OnAddLife()
                {
                    _currentHearts++;
                    UpdateHeartsUI(_currentHearts);
                }
                private void OnRemoveLife()
                {
                    _currentHearts--;
                    UpdateHeartsUI(_currentHearts);
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
        
                private void CreaateLifePoints()
                {
                    // Создание пула жизней
                    for (int i = 0; i < _maxHearts; i++)
                    {
                        var heart = Instantiate(_heartPrefab, _heartsContainer);
                        heart.gameObject.SetActive(i < _currentHearts);
                        _heartsPool.Add(heart);
                    }
                }
                private void UpdateHeartsUI(int currentLife)
                {
                    // Анимация добавления/отнимания
                    for (int i = 0; i < _maxHearts; i++)
                    {
                        if (i == currentLife - 1) // последнее добавленное
                        {
                            if(!_heartsPool[i].gameObject.activeSelf) 
                                _heartsPool[i].gameObject.SetActive(true);
                            _heartsPool[i].AnimateHeart(true).Forget();
                        }
                        else if (i == currentLife) // первое отнятое
                        {
                            _heartsPool[i].AnimateHeart(false).Forget();
                        };
                    }
                }
            }
        }