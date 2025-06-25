using __Project.Scripts.NewSystem.Fishes.Base;
using __Project.Scripts.NewSystem.Interfaces.Gameplays;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies
{
    public abstract class EnemyBase : BaseFish, ISpawnable, IMovable
    {
        public void SetSpeed(float newSpeed) => fishSpeed = newSpeed;
    
        protected float leftX => ScreenBoundsUtils.GetLeftScreenX();
        protected virtual float size => spriteRenderer?.sprite.bounds.size.x ?? 0f;
        public virtual void OnSpawn(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }
    
        public virtual void OnDespawn()
        {
            gameObject.SetActive(false);
        }
    
        public abstract void Move();
    
        protected void Update()
        {
            Move();
            if (transform.position.x <= leftX - size)
            {
                OnDespawn();
            }
        }
    }
}