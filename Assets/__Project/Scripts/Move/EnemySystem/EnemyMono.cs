using DG.Tweening;
using FishEscape.Enums;
using FishEscape.Fishs;
using Scripts.Buffer;
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

    public BubbleScale scaleFish { get; private set; }
    public Sprite Sprite => Fish.sprite;

    [ReadOnly]
    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private PolygonCollider2D polygonCollider;

    [SerializeField]
    private Camera cam;
    private float sizeFish;
    public float Size => sizeFish;

    private float scale;
    //private IEnumerator coroutine;
    //Vector2 rightButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
    //Vector2 reghtTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));
    private ITypeMove move;

    private List<IBuffer> listBuffer;
    public void Init(EnemyFish fish)
    {
        cam = Camera.main;
        this.Fish.DOFade(1f, .1f);

        this.Fish.sprite = fish.fish;
        this.speed = speed * 0.01f;
        this.sizeFish = fish.sizeFish;
        this.scale = fish.scale;

        polygonCollider = this.gameObject.AddComponent<PolygonCollider2D>();
        polygonCollider.isTrigger = true;

        scaleFish = GetComponent<BubbleScale>();
        scaleFish.size = sizeFish;

        listBuffer = new List<IBuffer>();
        foreach (var buffer in fish.list)
        {
                InitializeBuffer(buffer);
        }

        move = ChooseTypeMove(fish.typeMove);
    }
    public void InitializeBuffer(ListBuffer buffer)
    {
        switch (buffer)
        {
            case ListBuffer.ChangeAlphaFish: 
                var alpha = this.gameObject.AddComponent<ChangeAlphaFish>();
                listBuffer.Add(alpha);
                break;
            case ListBuffer.ScalerFish:
                var scale = this.gameObject.AddComponent<ScalerFish>();
                listBuffer.Add(scale);
                break;
            default: break;
        }
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


    private void OnEnable()
    {
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
        this.transform.DOKill();
        listBuffer.ForEach(x => x.ResetBuffer());
        Destroy(polygonCollider);
    }    
}
