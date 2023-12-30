using UnityEngine;
using FishEscape.Enums.Players;
using Sirenix.OdinInspector;

namespace FishEscape.Fishs
{
    public class Fish : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [HorizontalGroup("Basic Info/Horizontal")]
        [HideLabel]
        [PreviewField(150)]
        public Sprite fish;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [LabelWidth(100)]
        public string fishName;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [SerializeField] 
        private TypeFish type;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [LabelWidth(100)]
        [TextArea]
        public string description;

        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(0.5f, 5f)]
        [GUIColor(0.3f, 0.5f, 1f)]
        public float speed = 2f;


        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [Range(0.5f, 10f)]
        public float sizeFish = 1f;

        //[BoxGroup("Other Setting")]
        //public GameObject prefabs;

#if UNITY_EDITOR
        protected void Save()
        {
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}