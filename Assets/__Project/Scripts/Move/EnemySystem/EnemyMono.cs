using DG.Tweening;
using FishEscape.Enums;
using FishEscape.Fishs;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMono : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    public SpriteRenderer Fish
    {
        get
        {
            if(sprite == null)
                sprite = GetComponent<SpriteRenderer>();
            return sprite;
        }
    }
    public Sprite Sprite => Fish.sprite;

    [ReadOnly]
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private PolygonCollider2D polygonCollider;

    private Camera cam;
    private float sizeFish;
    public float Size => sizeFish;

    private float scale;
    //private IEnumerator coroutine;
    //Vector2 rightButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
    //Vector2 reghtTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
    private ITypeMove move;
    public void Init(EnemyFish fish)
    {
        this.Fish.sprite = fish.fish;
        this.speed = speed * 0.01f;
        this.sizeFish = fish.sizeFish;
        this.scale = fish.scale;

        polygonCollider = this.gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;

        foreach (var buffer in fish.list)
        {
            if (buffer != ListBuffer.None)
                this.gameObject.AddComponent(System.Type.GetType(buffer.ToString()));
        }
        move = ChooseTypeMove(fish.typeMove);
    }
    private ITypeMove ChooseTypeMove(EnumTypeMove type)
    {
        switch (type)
        {
            case EnumTypeMove.Line: return new LineMove(this.speed);
            case EnumTypeMove.Sin: return new SinMove(this.speed, this.frequency, this.amplitude);
            case EnumTypeMove.Cos: return new CosMove(this.speed, this.frequency, this.amplitude);
            default: return null;
        }
    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        cam = Camera.main;
        Fish.transform.localScale = Vector3.one * NewSize;
        this.transform.position = startPosition;
    }
    private float procent => cam.pixelHeight * 0.3f;
    Vector3 startPosition => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth + 150f, Random.Range(procent / 2, cam.pixelHeight - procent / 2), cam.nearClipPlane));
    Vector3 endPosition => (Vector2)cam.ScreenToWorldPoint(new Vector3(-12f, startPosition.y, cam.nearClipPlane));
    [SerializeField]
    private float amplitude = 0.006f;
    [SerializeField]
    private float frequency = 2;
    private void FixedUpdate()
    {
        if (this.transform.position.x > endPosition.x)
            this.transform.Translate(move.SpeedMove);
        else
            this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        //this.transform.DOKill();
        //StopCoroutine(coroutine);
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
            float spriteHeight = Sprite.rect.height
                               / Sprite.pixelsPerUnit;

            // How many times bigger (or smaller) is the height we want to fill?
            return visibleHeightAtDepth / spriteHeight;
        }
    }
    
}
