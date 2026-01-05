using System;
using System.Collections.Generic;
using __Project.Scripts.NewSystem.Fishes.Base;
using __Project.Scripts.NewSystem.Interfaces.Enemies;
using __Project.Scripts.NewSystem.Interfaces.Gameplays;
using __Project.Scripts.NewSystem.Tools;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Enemies
{
    public abstract class EnemyBase : BaseFish, ISpawnable
    {
        protected List<IMoveEnemy> _moveBehaviours = new();
        protected List<IEffectEnemy> _effectBehaviours = new();
        
        public void SetSpeed(float newSpeed) => fishSpeed = newSpeed;
        public float MaxSpeed { get; set; } = 10f;
        public float MinSpeed { get; set; } = 2f;
    
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

        protected abstract void Awake();
        protected virtual void Start()
        {
            foreach (var effect in _effectBehaviours)
            {
                effect.ApplyEffect(this);
            }
        }

        protected virtual void Move()
        {
            foreach (var move in _moveBehaviours)
            {
                move.Move(this);
            }
        }
    
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