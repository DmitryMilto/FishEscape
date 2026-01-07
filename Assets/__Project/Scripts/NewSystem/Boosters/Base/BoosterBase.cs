using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Fishes.Base;
using __Project.Scripts.NewSystem.Fishes.Players;
using UnityEngine;
using __Project.Scripts.NewSystem.Interfaces.Gameplays;

namespace __Project.Scripts.NewSystem.Boosters
{
    public abstract class BoosterBase : MonoBehaviour, ISpawnable, IMovable, IBoosterEffect, IOceanFish
    {
        [SerializeField] protected List<TypeOceans> _oceans;
        [SerializeField] protected float speed = 2f;

        public List<TypeOceans> Oceans => _oceans;

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
        }

        public void OnPickup()
        {
            this.gameObject.SetActive(false);
        }

        public abstract void ApplyEffect(PlayerFishBase player);

        public bool IsInOcean(TypeOceans ocean)
        {
            return _oceans.Contains(ocean);
        }
    }
}