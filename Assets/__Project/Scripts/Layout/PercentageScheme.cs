using Scripts.Enums.Layers;
using System;
using UnityEngine;

namespace Scripts.Layout
{
    [Serializable]
    public class PercentageScheme
    {
        [SerializeField, Range(0f, 150f)] private float percent;
        [SerializeField] private RectTransform transform;

        private RectTransform Transform
        {
            get
            {
                if (transform == null)
                {
                    Debug.LogError($"Transform is null in object");
                    return null;
                }
                return transform;
            }
            set
            {
                transform = value;
            }
        }
        public float Size { private get; set; }
        public float Percent { get => percent; }

        public void SizeDelta(TypeLayersGroup plane)
        {
            if (plane == TypeLayersGroup.Horizontal)
            {
                Vector2 vector = new Vector2(Size, Transform.rect.height);
                Debug.Log($"Layer {plane} Vector: {vector}");
                Transform.sizeDelta = vector;
            }
            else
            {
                if (plane == TypeLayersGroup.Vertical)
                {
                    Vector2 vector = new Vector2(Transform.rect.width, Size);
                    Debug.Log($"Layer {plane} Vector: {vector}");
                    Transform.sizeDelta = vector; 
                }
            }
        }
        public void InitSize()
        {
            Transform.sizeDelta = new Vector2(Size, Size);
        }
        public PercentageScheme(RectTransform transform)
        {
            this.Transform = transform;
        }
    }
}