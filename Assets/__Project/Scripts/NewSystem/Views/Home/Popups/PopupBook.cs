using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Elements;
using __Project.Scripts.NewSystem.Elements.UI.Books;
using __Project.Scripts.NewSystem.Elements.UI.Tabs;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Views.Base;
using FishEscape.Enums.Players;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace __Project.Scripts.NewSystem.Views.Home
{
    public class PopupBook : PopupBase
    {
        [SerializeField] private Transform _cardListParent;
        [SerializeField] private CollectionFishInfo _collectionFishInfo;

        [Header("Arrows")] [SerializeField] private Button _arrowLeft;
        [SerializeField] private Button _arrowRight;

        [Header("Tabs")] [SerializeField] private List<UITab> _tabs;
        [Header("Prefabs")] [SerializeField] private FishBookCard _cardPrefab;

        public event Action<string> OnChooseFish;
        public event Action<string> OnOpenFish;
        public event Action OnClickFish;
        public event Action<TypeFish> OnChooseTab;
        public Button ArrowLeft => _arrowLeft;
        public Button ArrowRight => _arrowRight;

        private List<FishBookCard> _cards = new();

        private const float CardSpacing = 400f; // фиксированный отступ между карточками

        private Vector2[] Positions => new Vector2[]
        {
            new(-CardSpacing / 2, CardSpacing / 2), // Лево-верх
            new(CardSpacing / 2, CardSpacing / 2), // Право-верх
            new(-CardSpacing / 2, -CardSpacing / 2), // Лево-низ
            new(CardSpacing / 2, -CardSpacing / 2) // Право-низ
        };

        public void Initialize(List<CardFishIcon> fishes, TypeFish fishType)
        {
            _collectionFishInfo.SetVisible(false);
            CreateCardsGrid(fishes);
            OnEditTabs(fishType);
        }

        public void UpdatePage(List<CardFishIcon> fishes)
        {
            CreateCardsGrid(fishes);
        }

        public void SwitchArrows(bool left, bool right)
        {
            _collectionFishInfo.SetVisible(false);
            ArrowLeft?.gameObject.SetActive(left);
            ArrowRight?.gameObject.SetActive(right);
        }

        public void OpenInfoFish(CollectionInfo collectionInfo)
        {
            _collectionFishInfo.SetVisible(true);
            _collectionFishInfo.SetInfo(collectionInfo);
        }

        protected override void Awake()
        {
            base.Awake();
            foreach (var tab in _tabs)
            {
                tab.OnChooseTab += OnTypeTabSelected;
            }
        }
        
        private void CreateCardsGrid(List<CardFishIcon> fishList)
        {
            foreach (Transform child in _cardListParent)
                Destroy(child.gameObject);
            _cards.Clear();

            var count = Mathf.Min(4, fishList.Count);

            for (var i = 0; i < count; i++)
            {
                var entry = fishList[i];
                if (entry == null) continue;
                var card = Instantiate(_cardPrefab, _cardListParent);
                card.Setup(entry);
                card.OnClickFish += HandlerClickFish;
                card.OnChooseFish += HandlerChooseFish;
                card.OnPreOpenFish += HandlerOpenFish;

                var rect = card.GetComponent<RectTransform>();
                rect.anchoredPosition = Positions[i];
                _cards.Add(card);
            }
        }

        private void HandlerChooseFish(string fishName) => OnChooseFish?.Invoke(fishName);
        private void HandlerOpenFish(string fishName) => OnOpenFish?.Invoke(fishName);
        private void HandlerClickFish() => OnClickFish?.Invoke();

        private void OnTypeTabSelected(TypeFish type)
        {
            HandlerClickFish();
            OnChooseTab?.Invoke(type);
        }
        
        private void OnEditTabs(TypeFish type)
        {
            foreach (var tab in _tabs)
            {
                tab.ChosenTab(type);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnClickFish = null;
            OnChooseFish = null;
            OnOpenFish = null;
            OnChooseTab = null;
        }
    }
}