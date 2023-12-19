using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [ReadOnly]
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private PolygonCollider2D polygonCollider;

    private Camera cam;
    private float sizeFish;
    Vector2 rightButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
    Vector2 reghtTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

    public void Init(Sprite sprite, float speed, float size, bool flipX)
    {
        this.sprite.sprite = sprite;
        this.speed = speed;
        this.sizeFish = size;

        if (flipX)
        {
            if (this.transform.rotation.y == 0) this.transform.localRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            else this.transform.localRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
        else
        {
            if (this.transform.rotation.y == 1) this.transform.localRotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
            else this.transform.localRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
        }
        polygonCollider = this.gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;
    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        cam = Camera.main;
        sprite.transform.localScale = Vector3.one * NewSize;
        var startPointTransform = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, Random.Range(0f, cam.pixelHeight), cam.nearClipPlane));
        this.transform.position = startPointTransform;
        var endPointPosition = (Vector2)cam.ScreenToWorldPoint(new Vector3(-50, startPointTransform.y, cam.nearClipPlane));
        this.transform.DOMoveX(endPointPosition.x, (float)(Vector2.Distance(startPointTransform, endPointPosition) / speed)).SetEase(Ease.Linear).OnComplete(() => this.gameObject.SetActive(false));
    }
    private void OnDisable()
    {
        this.transform.DOKill();
        Destroy(polygonCollider);
    }
    private float NewSize
    {
        get
        {
            // Angle the camera can see above the center.
            float halfFovRadians = cam.fieldOfView * Mathf.Deg2Rad / 2f;

            // How high is it from top to bottom of the view frustum,
            // in world space units, at our target depth?
            float visibleHeightAtDepth = this.sizeFish * Mathf.Tan(halfFovRadians) * 2f;

            // You could also use Sprite.bounds for this.
            float spriteHeight = sprite.sprite.rect.height
                               / sprite.sprite.pixelsPerUnit;

            // How many times bigger (or smaller) is the height we want to fill?
            return visibleHeightAtDepth / spriteHeight;

            // Scale to fit, uniformly on all axes.
            //spriteRenderer.transform.localScale = Vector3.one * scaleFactor;
            //Vector2 newSize;

            //var newHeight = cam.pixelHeight * procent;

            //newSize = new Vector2 (spriteRenderer.size.x, newHeight);
            //return newSize;
        }
    }
}
