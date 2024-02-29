using System.Collections.Generic;
using UnityEngine;

namespace Scripts.AutoSize
{
    public class SafeArea : MonoBehaviour, IAutoSize
    {
        private RectTransform rectTransform;
        public RectTransform Container => rectTransform;

        public float size { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void AutoSize()
        {
            UpdateSafeArea();
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //    AutoSize();
        //}

        public bool Horizontal { set { horizontal = value; } get { return horizontal; } }
        [SerializeField] private bool horizontal = true;

        public Vector2 HorizontalRange { set { horizontalRange = value; } get { return horizontalRange; } }
        [Range(0.0f, 1.0f)][SerializeField] private Vector2 horizontalRange = new Vector2(0.0f, 1.0f);

        /// <summary>Should you be able to drag vertically?</summary>
        public bool Vertical { set { vertical = value; } get { return vertical; } }
        [SerializeField] private bool vertical = true;

        public Vector2 VerticalRange { set { verticalRange = value; } get { return verticalRange; } }
        [Range(0.0f, 1.0f)][SerializeField] private Vector2 verticalRange = new Vector2(0.0f, 1.0f);

        [System.NonSerialized]
        private RectTransform cachedRectTransform;

        [System.NonSerialized]
        private bool cachedRectTransformSet;

        [ContextMenu("Update Safe Area")]
        public void UpdateSafeArea()
        {
            if (cachedRectTransformSet == false)
            {
                cachedRectTransform = GetComponent<RectTransform>();
                cachedRectTransformSet = true;
            }

            var safeRect = Screen.safeArea;
            var screenW = Screen.width;
            var screenH = Screen.height;
            var safeMin = safeRect.min;
            var safeMax = safeRect.max;

            if (horizontal == false)
            {
                safeMin.x = 0.0f;
                safeMax.x = screenW;
            }
            else
            {
                safeMin.x = Mathf.Max(safeMin.x, horizontalRange.x * screenW);
                safeMax.x = Mathf.Min(safeMax.x, horizontalRange.y * screenW);
            }

            if (vertical == false)
            {
                safeMin.y = 0.0f;
                safeMax.y = screenH;
            }
            else
            {
                safeMin.y = Mathf.Max(safeMin.y, verticalRange.x * screenH);
                safeMax.y = Mathf.Min(safeMax.y, verticalRange.y * screenH);
            }

            cachedRectTransform.anchorMin = new Vector2(safeMin.x / screenW, safeMin.y / screenH);
            cachedRectTransform.anchorMax = new Vector2(safeMax.x / screenW, safeMax.y / screenH);
        }
    }
}