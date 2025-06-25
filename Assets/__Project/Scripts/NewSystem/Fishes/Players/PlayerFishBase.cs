using DG.Tweening;
using UnityEngine;
using __Project.Scripts.NewSystem.Boosters;
using __Project.Scripts.NewSystem.Fishes.Base;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using __Project.Scripts.NewSystem.Tools;

namespace __Project.Scripts.NewSystem.Fishes.Players
{
    public class PlayerFishBase : BaseFish
    {
        [SerializeField] protected int maxLives = 3;
        public int MaxLives => maxLives;
        
        protected int currentLives;
        protected Camera mainCamera;
        private Sequence blinkSequence;
        private float blinkDuration = 1f;
        private bool isBlinking = false;
        protected Vector3? targetPosition = null;

        protected virtual void Start()
        {
            mainCamera = Camera.main;
            float leftX = ScreenBoundsUtils.GetLeftScreenX();
            transform.position = new Vector3(leftX + 1f, 0, 0);
            currentLives = maxLives;
        }

        protected virtual void Update()
        {
            HandleMovement();
        }

        protected virtual void HandleMovement()
        {
            float moveY = Input.GetAxisRaw("Vertical");
            if (moveY != 0)
            {
                targetPosition = null; // сброс цели при ручном управлении
                transform.Translate(Vector3.up * (moveY * fishSpeed * Time.deltaTime));
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    targetPosition = new Vector3(transform.position.x, mouseWorld.y, transform.position.z);
                }

                if (targetPosition != null)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        targetPosition.Value,
                        fishSpeed * Time.deltaTime
                    );

                    if (Mathf.Abs(transform.position.y - targetPosition.Value.y) < 0.01f)
                    {
                        targetPosition = null;
                    }
                }
            }
            // Ограничение по границам экрана
            var pos = transform.position;
            var min = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            var max = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            transform.position = pos;
        }
        public void AddLife(int amount)
        {
            currentLives = Mathf.Min(currentLives + amount, maxLives);
        }

        public void SetSpeedMultiplier(float multiplier, float duration)
        {
            fishSpeed *= multiplier;
            DOVirtual.DelayedCall(duration, () => fishSpeed /= multiplier);
        }

        public void SetInvincible(float duration)
        {
            StartBlink();
            DOVirtual.DelayedCall(duration, StopBlink);
        }

        public virtual void OnEnemyCollision(EnemyBase enemy)
        {
            if (!isBlinking)
            {
                StartBlink();
            }

            currentLives--;
            if (currentLives <= 0)
            {
                OnDeath();
            }
        }

        public virtual void HandleTriggerEnter(Collider2D other)
        {
            if (other.TryGetComponent<BoosterBase>(out var booster))
            {
                booster.ApplyEffect(this);
                booster.OnPickup(); // если нужно уничтожить буст
            }
            else if (other.TryGetComponent<EnemyBase>(out var enemy))
            {
                OnEnemyCollision(enemy);
            }
        }

        public virtual void HandleTriggerExit(Collider2D other)
        {
            if (isBlinking && other.GetComponent<EnemyBase>())
            {
                StopBlink();
            }
        }

        private void StartBlink()
        {
            isBlinking = true;
            blinkSequence = DOTween.Sequence();
            blinkSequence.Append(spriteRenderer.DOFade(0f, 0.15f))
                .Append(spriteRenderer.DOFade(1f, 0.15f))
                .SetLoops(-1, LoopType.Yoyo);

            DOVirtual.DelayedCall(blinkDuration, StopBlink);
        }

        private void StopBlink()
        {
            if (blinkSequence != null)
            {
                blinkSequence.Kill(true);
                blinkSequence = null;
            }

            spriteRenderer.DOFade(1f, 0f);
            isBlinking = false;
        }

        protected virtual void OnDeath()
        {
            StopBlink();
            gameObject.SetActive(false);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            HandleTriggerEnter(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            HandleTriggerExit(other);
        }
    }
}