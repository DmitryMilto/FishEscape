using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Enums;
using Cysharp.Threading.Tasks;

namespace __Project.Scripts.NewSystem.Elements
{
    public class FishBookCard : MonoBehaviour
    {
        [Header("UI Elements")] [SerializeField]
        private Image _icon;

        [SerializeField] private Image _lock;
        [SerializeField] private GameObject _active;
        [SerializeField] private Button _button;

        private CardFishIcon _entry;
        private FishCardState _state;
        public event Action<string> OnChooseFish;
        public event Action<string> OnPreOpenFish;
        public event Action OnClickFish;
        private Tween _preOpenTween;

        public void Setup(CardFishIcon entry, FishCardState state = FishCardState.Open)
        {
            _entry = entry;
            _state = state;
            _icon.sprite = entry.Icon;

            _button.interactable = state != FishCardState.Locked;

            SetColor(state);
            SetLockState(state);
        }

        public void GetOpenedFish(FishCardState state)
        {
            if (_state == state) return;
            if (state != FishCardState.PreOpen) return;
        }


        private void Awake()
        {
            _button.onClick.AddListener(ChooseFish);
        }

        private void ChooseFish()
        {
            OnClickFish?.Invoke();
            if (_state == FishCardState.PreOpen) OnPreOpenFish?.Invoke(_entry.FishId);
            if (_state == FishCardState.Open) OnChooseFish?.Invoke(_entry.FishId);
        }

        private void SetLockState(FishCardState state)
        {
            _lock.gameObject.SetActive(state != FishCardState.Open);
            if (state == FishCardState.PreOpen)
            {
                _preOpenTween?.Kill();
                _preOpenTween = _lock.transform.DOShakePosition(1f, 10f, 10, 90, false, true)
                    .SetLoops(-1);
            }
            else
            {
                _preOpenTween?.Kill();
            }
        }

        private void SetColor(FishCardState state)
        {
            _icon.color = _state == FishCardState.Open ? Color.white : Color.black;
        }

        private void OnDestroy()
        {
            _preOpenTween?.Kill();
            OnClickFish = null;
            OnChooseFish = null;
            OnPreOpenFish = null;
        }
    }
}