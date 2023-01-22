using UnityEngine;
using FishEscape.Enums.Players;
using Sirenix.OdinInspector;

namespace FishEscape.Fishs
{
    public class Fish : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        public string fishName;

        [BoxGroup("Basic Info")]
        [SerializeField] private TypeFish type;

        [BoxGroup("Basic Info")]
        [LabelWidth(100)]
        [TextArea]
        public string description;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(0.5f, 5f)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;
    }
}