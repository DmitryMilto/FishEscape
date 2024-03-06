using UnityEngine;
using FishEscape.Enums.Players;
using Sirenix.OdinInspector;

namespace FishEscape.Fishs
{
    public abstract class Fish : ScriptableObject
    {
        [BoxGroup("Basic Info")]
        [HorizontalGroup("Basic Info/Horizontal")]
        [HideLabel]
        [PreviewField(150)]
        [OnValueChanged("Update")]
        public Sprite fish;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [LabelWidth(100)]
        public string fishName;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [SerializeField]
        private TypeFish type;

        [VerticalGroup("Basic Info/Horizontal/Vertical")]
        [SerializeField]
        private EnumOcean habitat;
        public EnumOcean Habitat => habitat;

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

        [BoxGroup("Info for Collection")]
        public Sprite RealPhotoFish;

        [BoxGroup("Info for Collection")]
        [Multiline(3)]
        public string Description;

        [Space]
        public string Key;

        #region Abstract Method
        public abstract EnumStatusCard StatusCard { get; set; }
        public abstract void LoadData();
        public abstract void SaveData();
        public abstract void Update();
        #endregion

#if UNITY_EDITOR
        protected void Save()
        {
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }

        [Button]
        private void DeleteKeyInformation()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                Debug.Log($"<color='yellow'>Deleting date {this.Key}...</color>");
                PlayerPrefs.DeleteKey(Key);
                if (!PlayerPrefs.HasKey(Key)) Debug.Log($"<color='green'>Date {this.Key} was deleted successfully!</color>");
                else Debug.Log($"<color='red'>Date {this.Key} has not been deleted!</color>");
            }
            else
            {
                Debug.Log($"<color='red'>No data {this.Key} found!</color>");
            }
        }
#endif
    }
}