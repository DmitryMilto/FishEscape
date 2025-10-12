using UnityEngine;

namespace _Project.Game.Spawner
{
    public class SpawnZone : MonoBehaviour
    {
        [SerializeField] private Vector2 size = new(1f, 3f);

        public Vector3 GetRandomPosition()
        {
            var localPos = new Vector3(
                0f,
                Random.Range(-size.y / 2, size.y / 2),
                0f
            );
            return transform.position + localPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(1f, size.y, 0f));
        }
    }
}