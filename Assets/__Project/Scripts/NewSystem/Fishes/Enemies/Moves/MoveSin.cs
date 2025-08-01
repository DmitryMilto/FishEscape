using __Project.Scripts.NewSystem.Interfaces.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Moves
{
    public class MoveSin : IMoveEnemy
    {
        public float amplitude = 1f;
        public float frequency = 2f;
        private float phase;
        private float startY;
        private bool initialized = false;
        private bool isPaused = false;

        public MoveSin()
        {
            phase = Random.Range(0f, Mathf.PI * 2f);
            amplitude *= Random.Range(0.8f, 1.2f);
            frequency *= Random.Range(0.8f, 1.2f);
        }

        public void Move(EnemyBase enemy)
        {
            if (isPaused) return;
            if (!initialized)
            {
                startY = enemy.transform.position.y;
                initialized = true;
            }
            float y = startY + Mathf.Sin(Time.time * frequency + phase) * amplitude;
            enemy.transform.position = new Vector3(enemy.transform.position.x - enemy.Speed * Time.deltaTime, y, enemy.transform.position.z);
        }
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}