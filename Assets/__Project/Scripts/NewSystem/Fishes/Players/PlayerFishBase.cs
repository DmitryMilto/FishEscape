using System.Linq;
using DG.Tweening;
using UnityEngine;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Base;
using __Project.Scripts.NewSystem.Fishes.Enemies;
using __Project.Scripts.NewSystem.Fishes.Players.Providers;
using __Project.Scripts.NewSystem.Tools;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace __Project.Scripts.NewSystem.Fishes.Players
{
    public class PlayerFishBase : BaseFish
    {
        [SerializeField] protected int _startLives = 3;
        [SerializeField] protected int maxLives = 3;
        [SerializeField] private float invincibleDuration = 3f;

        public int MaxLives => maxLives;
        public int StartLives => _startLives;

        public int CurrentLives { get; set; }
        protected Camera mainCamera;
        protected Vector3? targetPosition = null;
        protected bool isPaused = false;

        private FishCollisionProvider _collisionProvider;
        private FishBlinkProvider _blinkProvider;
        private bool _isInvincible = false;
        public bool IsInvincible => _isInvincible;

        protected void Awake()
        {
            mainCamera = Camera.main;
            _blinkProvider = new FishBlinkProvider(this);
            _collisionProvider = new FishCollisionProvider(this);
        }

        protected virtual void Start()
        {
            float leftX = ScreenBoundsUtils.GetLeftScreenX();
            transform.position = new Vector3(leftX + 4f, 0, 0);
            CurrentLives = maxLives;
        }

        protected virtual void Update()
        {
            HandleMovement();
        }

        protected virtual void HandleMovement()
        {
            if(EventSystem.current.IsPointerOverGameObject()) return;
            if (isPaused) return;
            float moveY = Input.GetAxisRaw("Vertical");
            if (moveY != 0)
            {
                targetPosition = null;
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
            var pos = transform.position;
            var min = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
            var max = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));
            pos.y = Mathf.Clamp(pos.y, min.y + 2f, max.y - 2f);
            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            transform.position = pos;
        }

        public void SetPause(bool pause)
        {
            isPaused = pause;
            if (pause && targetPosition != null)
            {
                targetPosition = null;
            }
        }
        public void AddLife(int amount)
        {
            CurrentLives = Mathf.Min(CurrentLives + amount, maxLives);
            GlobalEventsManager.AddLife();
        }

        public void SetSpeedMultiplier(float multiplier, float duration)
        {
            fishSpeed *= multiplier;
            DOVirtual.DelayedCall(duration, () => fishSpeed /= multiplier);
        }

        public virtual void OnEnemyExit(EnemyBase enemy)
        {
            // Можно добавить логику при выходе из врага, если нужно
        }

        public async UniTaskVoid StartInvincibilityAsync()
        {
            _isInvincible = true;
            _blinkProvider.StartBlink();
            await UniTask.Delay((int)(invincibleDuration * 1000), cancellationToken: this.GetCancellationTokenOnDestroy());
            _blinkProvider.StopBlink();
            _isInvincible = false;

            // Если всё ещё на враге — получить урон снова
            if (_collisionProvider.IsTouchingEnemy)
            {
                var enemy = _collisionProvider.CurrentEnemies.FirstOrDefault();
                if (enemy != null)
                {
                    _collisionProvider.OnEnemyCollision(enemy);
                }
            }
        }

        protected virtual void OnDeath()
        {
            _blinkProvider.StopBlink();
            gameObject.SetActive(false);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            _collisionProvider.HandleTriggerEnter(other);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            _collisionProvider.HandleTriggerExit(other);
        }

        public override ENamesFish Name => ENamesFish.Player_Salmon; // Переопределяем имя рыбы, если нужно
    }
}