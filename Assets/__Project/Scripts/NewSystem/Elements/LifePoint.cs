using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Elements
{
    public class LifePoint : MonoBehaviour
    {
        [SerializeField] private Image image;
        private Material _material;
        private readonly string _property = "_FullGlowDissolveFade";
        public int Id { get; private set; }
        public bool IsOpened { get; private set; }

        private void Awake()
        {
            _material = Instantiate(image.material);
            image.material = _material;
            _material.SetFloat(_property, 0f);
        }

        public void Init(int id, bool opened = false)
        {
            Id = id;
            AnimateHeart(opened);
        }

        public async UniTask SetStateAsync(int currentLives, CancellationToken cancellationToken = default)
        {
            var shouldBeOpened = Id == currentLives;
            if (!shouldBeOpened) return;

            await AnimateHeartAsync(cancellationToken);
        }

        public void ResetState(bool opened)
        {
            AnimateHeart(opened);
        }

        /// <summary>
        /// Анимация открытия/закрытия сердца
        /// </summary>
        private void AnimateHeart(bool opened)
        {
            IsOpened = opened;
            var target = opened ? 1f : 0f;
            _material.SetFloat(_property, target);
        }

        private async UniTask AnimateHeartAsync(CancellationToken cancellationToken = default)
        {
            var target = IsOpened ? 0f : 1f;
            await _material.DOFloat(target, _property, 0.5f)
                .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
            IsOpened = !IsOpened;
        }
    }
}