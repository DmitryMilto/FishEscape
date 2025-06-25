using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies.Types
{
    public class EnemySin : EnemyBase
    {
        public float amplitude = 1f;
        public float frequency = 2f;
        private float startY;
    
        public override void OnSpawn(Vector3 position)
        {
            base.OnSpawn(position);
            startY = position.y;
        }
    
        public override void Move()
        {
            float y = startY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime, y, transform.position.z);
        }
    }

}