using __Project.Scripts.NewSystem.Enums;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Fishes.Base
{
    public abstract class BaseFish : MonoBehaviour
    {
#if UNITY_EDITOR && ALL_DEBUG
        protected string _nameLog = $"<color=red>[{nameof(BaseFish)}]</color>";
#else
        protected string _nameLog = $"[{nameof(BaseFish)}]";
#endif
        [SerializeField] protected SpriteRenderer spriteRenderer;
        public SpriteRenderer Sprite => spriteRenderer;
        
        [SerializeField] protected TypeOceans oceans;
        public TypeOceans Oceans => oceans;
        
        [SerializeField] protected ENamesFish nameFish;
        public ENamesFish Name => nameFish;
        
        [SerializeField] protected float fishSpeed;
        public float Speed => fishSpeed;
    }
}