using DG.Tweening;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Types
{
    public class EnemyBlink : EnemyBase
    {
        [Header("Spawn Settings")]
        public float blinkInterval = 2f;
        private float timer;

        private Sequence blinkSequence;
        
        public override void Move()
        {
            transform.Translate(Vector3.left * (Speed * Time.deltaTime));
            timer += Time.deltaTime;
            if (timer > blinkInterval)
            {
                StartBlinkAnimation();
                timer = 0;
            }
        }
        private void StartBlinkAnimation()
        {
            if (blinkSequence != null && blinkSequence.IsActive()) return;

            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(spriteRenderer.DOFade(0f, 1f))
                .Append(Sprite.DOFade(1f, 0.5f))
                .SetLoops(-1, LoopType.Yoyo);
        }

        public override void OnDespawn()
        {
            if (blinkSequence != null)
            {
                blinkSequence.Kill(true);
                blinkSequence = null;
            }
            base.OnDespawn();
        }
    }
}