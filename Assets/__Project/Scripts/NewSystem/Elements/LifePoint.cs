using System;
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

        private void Awake()
        {
            // Клонируем материал для каждого экземпляра
            _material = Instantiate(image.material);
            image.material = _material;
            
            _material.SetFloat(_property, 0f);
            AnimateHeart(true).Forget();
        }

        public async UniTaskVoid AnimateHeart(bool added)
        {
            if (added)
            {
                await _material.DOFloat(1f, _property, 0.5f)
                    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait);
            }
            else
            {
                await _material.DOFloat(0f, _property, 0.5f)
                    .ToUniTask(TweenCancelBehaviour.KillAndCancelAwait);
            }
        }
    }
}