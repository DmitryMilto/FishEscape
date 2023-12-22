using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
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

    private float scale;
    private IEnumerator coroutine;
    Vector2 rightButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
    Vector2 reghtTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

    public void Init(Sprite sprite, float speed, float size, float scale,bool flipX)
    {
        this.sprite.sprite = sprite;
        this.speed = speed;
        this.sizeFish = size;
        this.scale = scale;

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
        var procent = cam.pixelHeight * 0.3f;
        var startPointTransform = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + 150f, Random.Range(procent / 2, cam.pixelHeight - procent / 2), cam.nearClipPlane));
        this.transform.position = startPointTransform;
        var endPointPosition = (Vector2)cam.ScreenToWorldPoint(new Vector3(-50, startPointTransform.y, cam.nearClipPlane));
        this.transform.DOMoveX(endPointPosition.x, (float)(Vector2.Distance(startPointTransform, endPointPosition) / speed)).SetEase(Ease.Linear).OnComplete(() => this.gameObject.SetActive(false));
        coroutine = Scale();
        StartCoroutine(coroutine);
    }
    private void OnDisable()
    {
        this.transform.DOKill();
        StopCoroutine(coroutine);
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
        }
    }
    private IEnumerator Scale()
    {
        while (true)
        {
            SetScale(scale);
            yield return new WaitForSeconds(.5f);
            SetScale(0f);
            yield return new WaitForSeconds(.5f);
        }
    }
    private void SetScale(float value)
    {
        this.transform.DOScale(NewSize + value, .5f).SetEase(Ease.Linear);
    }
}
