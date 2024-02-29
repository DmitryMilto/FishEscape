using UnityEngine;

namespace Scripts.AutoSize
{
    public interface IAutoSize
    {
        public RectTransform Container { get; }
        public float size { get; set; }
        public void AutoSize();
    }
}