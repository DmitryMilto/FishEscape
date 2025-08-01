using __Project.Scripts.NewSystem.Interfaces.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Moves
{
    public class MoveZigZag : IMoveEnemy
    {
        public float amplitude = 1f;
        public float frequency = 3f;
        private float startY;
        private bool initialized = false;
        private bool isPaused = false;

        public void Move(EnemyBase enemy)
        {
            if (isPaused) return;
            if (!initialized)
            {
                startY = enemy.transform.position.y;
                initialized = true;
            }
            float y = startY + Mathf.PingPong(Time.time * frequency, amplitude * 2) - amplitude;
            enemy.transform.position = new Vector3(enemy.transform.position.x - enemy.Speed * Time.deltaTime, y, enemy.transform.position.z);
        }
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}