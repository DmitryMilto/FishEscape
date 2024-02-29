using Scripts.Enums.Layers;
using Scripts.Layout;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.AutoSize
{
    public class AutoWindowSize : MonoBehaviour, IAutoSize
    {
        private RectTransform baseContainer;
        [OnValueChanged("UpdateComponent")]
        [SerializeField] private TypeLayersGroup _plane;
        [SerializeField] private TextAnchor _anchor = TextAnchor.MiddleCenter;
        [SerializeField] private List<PercentageScheme> ratios;

        public RectTransform Container
        {
            get => this.baseContainer != null ? this.baseContainer : this.baseContainer = this.GetComponent<RectTransform>();
        }
        public float size
        {
            get => _plane == TypeLayersGroup.Horizontal ? (float)Container.rect.width : (float)Container.rect.height;
            set => throw new NotImplementedException();
        }
        [Button]
        private void PutSize()
        {
            foreach (var ratio in ratios)
            {
                ratio.Size = ratio.Percent * size / 100f;
                ratio.SizeDelta(_plane);
            }
        }

        public void AutoSize() => PutSize();

        #region Editor 
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (ratios.Count == 0)
            {
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    ratios.Add(new PercentageScheme(this.transform.GetChild(i).GetComponent<RectTransform>()));
                }
            }
        }
        private void UpdateComponent()
        {
            HorizontalOrVerticalLayoutGroup _newComponent;
            if (_plane == TypeLayersGroup.Vertical)
            {
                if (this.TryGetComponent<HorizontalLayoutGroup>(out var component))
                    DestroyImmediate(component);
                _newComponent = this.gameObject.AddComponent<VerticalLayoutGroup>();
            }
            else
            {
                if (_plane == TypeLayersGroup.Horizontal)
                    if (this.TryGetComponent<VerticalLayoutGroup>(out var component))
                        DestroyImmediate(component);
                _newComponent = this.gameObject.AddComponent<HorizontalLayoutGroup>();
            }
            _newComponent.childAlignment = _anchor;
        }
#endif
        #endregion
    }
}
