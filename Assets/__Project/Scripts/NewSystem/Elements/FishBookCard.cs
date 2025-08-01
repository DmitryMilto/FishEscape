using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using __Project.Scripts.NewSystem.Enums;

namespace __Project.Scripts.NewSystem.Elements
{
    public class FishBookCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _active;
        
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _preOpenEffect;
        [SerializeField] private Button _button;

        private FishBookEntry _entry;
        private FishCardState _state;
        private Action<FishBookEntry> _onClick;
        private Tween _preOpenTween;

        public void Setup(FishBookEntry entry, FishCardState state, Action<FishBookEntry> onClick)
        {
            _entry = entry;
            _state = state;
            _onClick = onClick;
            _icon.sprite = entry.gameFishSprite;
            _active.SetActive(state == FishCardState.Open);
            _lock.SetActive(state == FishCardState.Locked);
            _preOpenEffect.SetActive(state == FishCardState.PreOpen);

            _button.interactable = state == FishCardState.Open;
            _button.onClick.RemoveAllListeners();
            if (_button.interactable)
                _button.onClick.AddListener(() => _onClick?.Invoke(_entry));

            if (state == FishCardState.PreOpen)
            {
                _preOpenTween?.Kill();
                _preOpenTween = _lock.transform.DOShakePosition(1f, 10f, 10, 90, false, true)
                    .SetLoops(-1);
                // Можно добавить свечение через DoTween
            }
            else
            {
                _preOpenTween?.Kill();
            }
        }

        private void OnDestroy()
        {
            _preOpenTween?.Kill();
        }
    }
}