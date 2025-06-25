using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Types
{
    public class EnemyStraight : EnemyBase
    {
        public override void Move()
        {
            transform.Translate(Vector3.left * (Speed * Time.deltaTime));
        }
    }
}