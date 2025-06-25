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

        public static Vector2 GetOffset(TypeAnimation direction, float distance)
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

        public static async UniTask AnimateRectAsync(RectTransform rect,Vector2 original, TypeAnimation direction, bool toCenter, bool isView = false)
        {
            float distance = isView ? _viewDistance : _subPartDistance;
            float duration = isView ? _viewDuration : _subPartDuration;

            var canvasGroup = rect.GetComponent<CanvasGroup>() ?? rect.gameObject.AddComponent<CanvasGroup>();
            Vector2 offset = GetOffset(direction, distance);

            Vector2 from = toCenter ? original + offset : original;
            Vector2 to = toCenter ? original : original + offset;
            float fadeFrom = toCenter ? 0 : 1;
            float fadeTo = toCenter ? 1 : 0;

            float t = 0;
            while (t < duration)
            {
                float progress = t / duration;
                rect.anchoredPosition = Vector2.Lerp(from, to, progress);
                canvasGroup.alpha = Mathf.Lerp(fadeFrom, fadeTo, progress);
                t += Time.deltaTime;
                await UniTask.Yield();
            }
            rect.anchoredPosition = to;
            canvasGroup.alpha = fadeTo;
        }
    }
}