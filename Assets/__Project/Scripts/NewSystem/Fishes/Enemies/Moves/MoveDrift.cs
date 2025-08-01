using __Project.Scripts.NewSystem.Interfaces.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Moves
{
    public class MoveDrift : IMoveEnemy
    {
        public float driftStrength = 0.5f;
        private Vector2 driftDir;
        private bool initialized = false;
        private bool isPaused = false;

        public void Move(EnemyBase enemy)
        {
            if (isPaused) return;
            if (!initialized)
            {
                driftDir = Random.insideUnitCircle.normalized * driftStrength;
                initialized = true;
            }
            Vector3 move = new Vector3(-enemy.Speed * Time.deltaTime, driftDir.y * Time.deltaTime, 0f);
            enemy.transform.position += move;
        }
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}