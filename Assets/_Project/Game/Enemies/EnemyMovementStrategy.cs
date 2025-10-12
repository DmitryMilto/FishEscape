using UnityEngine;

namespace _Project.Game.Enemies
{
    public abstract class EnemyMovementStrategy : ScriptableObject
    {
        public abstract void Move(Transform transform, float speed);
    }

    [CreateAssetMenu(menuName = "Enemy/Move/Straight")]
    public class StraightMove : EnemyMovementStrategy
    {
        public override void Move(Transform transform, float speed)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }

    [CreateAssetMenu(menuName = "Enemy/Move/Zigzag")]
    public class ZigzagMove : EnemyMovementStrategy
    {
        public float frequency = 2f;
        public float magnitude = 0.5f;

        public override void Move(Transform transform, float speed)
        {
            float wave = Mathf.Sin(Time.time * frequency) * magnitude;
            transform.position += new Vector3(-speed, wave, 0f) * Time.deltaTime;
        }
    }
}