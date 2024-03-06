using Scripts.Bonus.Enum;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Bonus.Database {
    public abstract class EmptyDatabaseBonus<T> : ScriptableObject
    {
        [PreviewField(60)]
        [HideLabel]
        public Sprite element;

        [EnumPaging]
        public TypeBubble TypeBubble;

        [EnumPaging]
        public T TypeBonus;

        [Range(.5f, 1.5f)]
        public float scale;
    }

}