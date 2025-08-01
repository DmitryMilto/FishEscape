using __Project.Scripts.NewSystem.Interfaces.Enemies;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Moves
{
    public class MoveStraight : IMoveEnemy
    {
        private bool isPaused = false;
        
        public void Move(EnemyBase enemy)
        {
            if (isPaused) return;
            enemy.transform.Translate(Vector3.left * (enemy.Speed * Time.deltaTime));
        }
        
        public void Pause() => isPaused = true;
        public void Resume() => isPaused = false;
    }
}