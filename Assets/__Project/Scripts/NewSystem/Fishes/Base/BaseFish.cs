using System.Collections.Generic;
using __Project.Scripts.NewSystem.Enums;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Base
{
    public abstract class BaseFish : MonoBehaviour, IOceanFish
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=red>[{nameof(BaseFish)}]</color>";
#else
        protected string _nameLog = $"[{nameof(BaseFish)}]";
#endif
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected float fishSpeed;
        [SerializeField] List<TypeOceans> _oceans;
        public SpriteRenderer Sprite => spriteRenderer;
        public List<TypeOceans> Oceans => _oceans;
        public abstract ENamesFish Name { get; }

        public float Speed => fishSpeed;

        public bool IsInOcean(TypeOceans ocean)
        {
            return Oceans.Contains(ocean);
        }
    }
}