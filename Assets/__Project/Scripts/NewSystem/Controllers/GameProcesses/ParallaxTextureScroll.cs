using UnityEngine;

namespace __Project.Scripts.NewSystem.Controllers.GameProcesses
{
    public class ParallaxTextureScroll : MonoBehaviour
    {
        private static readonly int Offset = Shader.PropertyToID("_Offset");
        public float speed = 0.1f;
        public float parallaxFactor = 1f;
        private Material mat;
        private Vector2 offset;

        void Start()
        {
            mat = GetComponent<SpriteRenderer>().material;
            offset = mat.GetVector(Offset);
        }

        void Update()
        {
            offset.x += speed * parallaxFactor * Time.deltaTime;
            // Оставляем только дробную часть для seamless tile
            float x = offset.x - Mathf.Floor(offset.x);
            float y = offset.y - Mathf.Floor(offset.y);
            mat.SetVector(Offset, new Vector4(x, y, 0, 0));
        }
    }
}