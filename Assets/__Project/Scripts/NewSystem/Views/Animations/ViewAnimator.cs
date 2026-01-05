using System.Threading;
using __Project.Scripts.NewSystem.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Views.Animations
{
    public static class ViewAnimator
    {
        private static float _subPartDistance = 100f;
        private static float _subPartDuration = 0.3f;
        private static float _viewDistance = 50f;
        private static float _viewDuration = 0.3f;

        private static Vector2 GetOffset(TypeAnimation direction, float distance)
        {
            return direction switch
            {
                TypeAnimation.Up => new Vector2(0, distance),
                TypeAnimation.Down => new Vector2(0, -distance),
                TypeAnimation.Left => new Vector2(-distance, 0),
                TypeAnimation.Right => new Vector2(distance, 0),
                _ => Vector2.zero
            };
        }

        public static async UniTask AnimateRectAsync(RectTransform rect,Vector2 original, TypeAnimation direction, bool toCenter, bool isView = false, CancellationToken cancellationToken = default)
        {
            var distance = isView ? _viewDistance : _subPartDistance;
            var duration = isView ? _viewDuration : _subPartDuration;

            var canvasGroup = rect.GetComponent<CanvasGroup>() ?? rect.gameObject.AddComponent<CanvasGroup>();
            var offset = GetOffset(direction, distance);

            var from = toCenter ? original + offset : original;
            var to = toCenter ? original : original + offset;
            var fadeFrom = toCenter ? 0f : 1f;
            var fadeTo = toCenter ? 1f : 0f;

            var t = 0f;
            while (t < duration)
            {
                var progress = t / duration;
                rect.anchoredPosition = Vector2.Lerp(from, to, progress);
                canvasGroup.alpha = Mathf.Lerp(fadeFrom, fadeTo, progress);
                t += Time.deltaTime;
                await UniTask.Yield(cancellationToken: cancellationToken);
            }
            rect.anchoredPosition = to;
            canvasGroup.alpha = fadeTo;
        }
    }
}