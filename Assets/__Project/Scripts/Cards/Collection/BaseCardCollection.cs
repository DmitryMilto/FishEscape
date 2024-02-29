using Doozy.Runtime.UIManager.Components;
using FishEscape.Fishs;
using Scripts.AutoSize;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Card.Collection
{
    public abstract class BaseCardCollection<T> : UIAutoSizeController where T : Fish
    {
        public T Card { get; protected set; }
        [SerializeField]
        protected Image fish;

        [Title("Status")]
        [EnumToggleButtons]
        [SerializeField]
        protected EnumStatusCard status;

        private IShader cardShader;
        protected IShader CardShader
        {
            get
            {
                if(cardShader == null)
                {
                    cardShader = GetComponentInChildren<IShader>();
                    cardShader.InitShader();
                }
                return cardShader;
            }
        }

        public UIButton button { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            button = GetComponent<UIButton>();
        }
        private ControllerBook book;
        public void SetPageCollection(ControllerBook book)
        {
            this.book = book;
        }
        [Button]
        private void OpenCard()
        {
            StartCoroutine(Time());
        }
        private void ClickDetailsInfo()
        {
            book.Collection.SetFish(this.Card);
        }
        private IEnumerator Time()
        {
            this.Card.StatusCard = EnumStatusCard.PreOpen;
            yield return cardShader?.UpdateShader();
            this.Card.StatusCard = EnumStatusCard.Open;
            SwitchEventCard();
        }
        private void OnDisable()
        {
            cardShader?.DisableShader();
            this.button.onClickEvent.RemoveListener(OpenCard);
        }

        public abstract void SetCard(T card);
        protected void InitCard()
        {
            this.fish.sprite = this.Card.fish;
            this.status = this.Card.StatusCard;

            SwitchEventCard();
        }
        private void SwitchEventCard()
        {
            this.button.onClickEvent.RemoveAllListeners();
            switch (this.Card.StatusCard)
            {
                case EnumStatusCard.PreClose:
                case EnumStatusCard.PreOpen:
                    this.CardShader.SetActive(true);
                    button.onClickEvent.AddListener(OpenCard);
                    break;
                case EnumStatusCard.Open:
                    this.CardShader.SetActive(false);
                    button.onClickEvent.AddListener(ClickDetailsInfo);
                    break;
                case EnumStatusCard.Close:
                    this.CardShader.SetActive(true);
                    break;

                default:
                    break;
            }
        }
    }
}