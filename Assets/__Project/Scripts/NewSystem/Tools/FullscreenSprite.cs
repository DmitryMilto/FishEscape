using UnityEngine;
            
namespace __Project.Scripts.NewSystem.Tools
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FullscreenTiledSprite : MonoBehaviour
    {
        void Start()
        {
            var sr = GetComponent<SpriteRenderer>();
            var cam = Camera.main;

            // Размеры видимой области камеры в мировых координатах
            float worldScreenHeight = cam.orthographicSize * 2f;
            float worldScreenWidth = worldScreenHeight * cam.aspect;

            // Устанавливаем размер SpriteRenderer (работает только для Draw Mode: Tiled)
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size = new Vector2(worldScreenWidth, worldScreenHeight);

            // Сбросить масштаб, чтобы не было искажений
            transform.localScale = Vector3.one;
        }
    }
}