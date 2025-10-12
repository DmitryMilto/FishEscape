using _Project.Core.Utils;
using UnityEngine;
using _Project.Data;
using _Project.Game.Player;

namespace _Project.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FishStats stats;
        [SerializeField] private Collider2D playerCollider;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float verticalSpeed = 5f;

        private bool _isActive;

        public FishStats GetStats() => stats;
        public Collider2D Collider => playerCollider;

        public void Initialize()
        {
            _isActive = true;
            stats.ResetStats();
            rb.simulated = true;
            playerCollider.enabled = true;
            gameObject.SetActive(true);

            TDebug.Log("[Player] Initialized.");
        }

        public void Disable()
        {
            _isActive = false;
            rb.velocity = Vector2.zero;
            rb.simulated = false;
            playerCollider.enabled = false;
            gameObject.SetActive(false);

            TDebug.Log("[Player] Disabled.");
        }

        private void Update()
        {
            if (!_isActive) return;

            if (Input.GetMouseButton(0))
            {
                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float direction = worldPos.y > transform.position.y ? 1f : -1f;

                rb.velocity = new Vector2(rb.velocity.x, direction * verticalSpeed * stats.Speed);
            }
        }

        public void TakeDamage(int amount)
        {
            stats.Lives -= amount;
            if (stats.Lives <= 0)
            {
                TDebug.Log("[Player] Dead.");
                Disable();
                // You may call GameManager.OnPlayerDied();
            }
        }
    }
}