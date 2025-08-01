using __Project.Scripts.NewSystem.Interfaces.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Moves
{
    public class MoveCircle : IMoveEnemy
    {
        public float radius = 1f;
        public float angularSpeed = 2f;
        private Vector3 center;
        private float angle;
        private bool initialized = false;
        private bool isPaused = false;

        public void Move(EnemyBase enemy)
        {
            if (isPaused) return;
            if (!initialized)
            {
                center = enemy.transform.position;
                angle = 0f;
                initialized = true;
            }
            angle += angularSpeed * Time.deltaTime;
            float x = center.x - enemy.Speed * Time.deltaTime;
            float y = center.y + Mathf.Sin(angle) * radius;
            float z = center.z + Mathf.Cos(angle) * radius * 0.2f;
            enemy.transform.position = new Vector3(x, y, z);
            center.x = x; // центр смещается влево
        }
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}