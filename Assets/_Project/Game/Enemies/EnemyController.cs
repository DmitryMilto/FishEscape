using UnityEngine;
using _Project.Core.Utils;
using _Project.Data.Enemies;

namespace _Project.Game.Enemies
{
    public class EnemyController : MonoBehaviour
    {
        private EnemyData _data;
        private float _speed;

        public void Init(EnemyData data)
        {
            _data = data;
            _speed = data.speed;
            TDebug.Log($"Enemy spawned: {data.displayName}");
        }

        private void Update()
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }

        public float Radius => _data.size;
    }
}