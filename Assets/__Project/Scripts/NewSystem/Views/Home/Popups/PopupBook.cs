using System.Collections.Generic;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Elements;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Views.Base;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using VContainer;
namespace __Project.Scripts.NewSystem.Views.Home
{
    public class PopupBook : PopupBase
    {
        [SerializeField] private List<Button> _tabButtons;
        [SerializeField] private Transform _cardListParent;
        [SerializeField] private FishBookCard _cardPrefab;
        [SerializeField] private Image _oceanBackground;
        [SerializeField] private Image _gameFishImage;
        [SerializeField] private Image _realFishImage;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _arrowLeft;
        [SerializeField] private Button _arrowRight;

        [Inject] private FishBookManager _bookManager;
        private FishType _currentFishType = FishType.Player; // по умолчанию

        private List<FishBookCard> _cards = new();
        
        private int _currentOceanTabIdx = 0;
        private const float CardSpacing = 400f; // фиксированный отступ между карточками

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _tabButtons.Count; i++)
            {
                int idx = i;
                _tabButtons[i].onClick.AddListener(() => OnTabSelected(idx));
            }
            _arrowLeft.onClick.AddListener(OnArrowLeft);
            _arrowRight.onClick.AddListener(OnArrowRight);
        }

        public override async UniTask OpenAsync()
        {
            await base.OpenAsync();
            OnTabSelected(1);
        }

        private void OnTabSelected(int tabIdx)
        {
            _currentOceanTabIdx = tabIdx;
            List<FishBookEntry> fishList;
            if (!System.Enum.IsDefined(typeof(TypeOceans), tabIdx))
                fishList = _bookManager.GetAllFishByType(_currentFishType);
            else
                fishList = _bookManager.GetFishByOceanAndType((TypeOceans)tabIdx, _currentFishType);

            CreateCardsGrid(fishList);
        }
        private void CreateCardsGrid(List<FishBookEntry> fishList)
        {
            foreach (Transform child in _cardListParent)
                Destroy(child.gameObject);
            _cards.Clear();

            int count = Mathf.Min(4, fishList.Count);
            Vector2[] positions = {
                new(-CardSpacing/2,  CardSpacing/2),  // Лево-верх
                new( CardSpacing/2,  CardSpacing/2),  // Право-верх
                new(-CardSpacing/2, -CardSpacing/2),  // Лево-низ
                new( CardSpacing/2, -CardSpacing/2)   // Право-низ
            };

            for (int i = 0; i < count; i++)
            {
                var entry = fishList[i];
                var card = Instantiate(_cardPrefab, _cardListParent);
                card.Setup(entry, _bookManager.GetCardState(entry.fishId), OnCardClicked);

                var rect = card.GetComponent<RectTransform>();
                rect.anchoredPosition = positions[i];
                _cards.Add(card);
            }
        }
        private void OnTypeTabSelected(int typeIdx)
        {
            _currentFishType = (FishType)typeIdx;
            OnTabSelected(_currentOceanTabIdx);
        }

        private void OnCardClicked(FishBookEntry entry)
        {
            _oceanBackground.sprite = entry.oceanBackground;
            _gameFishImage.sprite = entry.gameFishSprite;
            _realFishImage.sprite = entry.realFishSprite;
            _description.text = entry.description;
        }

        private void OnArrowLeft() => _bookManager.ShowPrevFish();
        private void OnArrowRight() => _bookManager.ShowNextFish();
    }
}