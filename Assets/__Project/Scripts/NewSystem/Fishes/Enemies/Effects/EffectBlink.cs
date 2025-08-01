using __Project.Scripts.NewSystem.Interfaces.Enemies;
using DG.Tweening;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Effects
{
    public class EffectBlink : IEffectEnemy
    {
        private Sequence blinkSequence;
        private bool isPaused;

        public void ApplyEffect(EnemyBase enemy)
        {
            if (blinkSequence != null && blinkSequence.IsActive()) return;
            var sr = enemy.Sprite;
            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(sr.DOFade(0f, 1f))
                .Append(sr.DOFade(1f, 1f))
                .SetLoops(-1, LoopType.Yoyo);
            if (isPaused) blinkSequence.timeScale = 0;
        }

        public void RemoveEffect(EnemyBase enemy)
        {
            blinkSequence?.Kill(true);
            blinkSequence = null;
            enemy.Sprite.DOFade(1f, 0f);
        }

        public void Pause()
        {
            isPaused = true;
            blinkSequence?.Pause();
        }

        public void Resume()
        {
            isPaused = false;
            blinkSequence?.Play();
        }
    }
}