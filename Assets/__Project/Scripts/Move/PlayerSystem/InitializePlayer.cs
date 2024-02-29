using DG.Tweening;
using Doozy.Runtime.Colors;
using FishEscape.Fishs;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    [Title("Player")]
    [OnValueChanged("SetPlayer")]
    
    [SerializeField] private PlayerFish player;

    [Title("Other setting")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Camera cam;

    //Vector2 leftButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(0,0,cam.nearClipPlane));
    //Vector2 lefTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
    //Vector2 rightButton => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
    //Vector2 reghtTop => (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));

    public void SetPlayer(PlayerFish fish)
    {
        if (fish == null) return;
        player = fish;
        spriteRenderer.sprite = player.fish;
    }
    private void Start()
    {
        if(cam == null) cam = Camera.main;
        //Debug.Log($"Wigth {cam.pixelWidth} and Height {cam.pixelHeight}");

        var newHalfWidth = cam.pixelWidth * 0.2f;
        var newHalfHeigth = (cam.pixelHeight / 2);
        //Debug.Log($"Wigth {newHalfWidth} and Height {newHalfHeigth}");
        spriteRenderer.transform.position = (Vector2)cam.ScreenToWorldPoint(new Vector3(newHalfWidth, newHalfHeigth, cam.nearClipPlane));

        GetComponentInChildren<MovePlayer>().speed = player.speed;

        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.transform.localScale = Vector3.one * NewSize(player.sizeFish);
        // spriteRenderer.size = NewSize();
    }

    private float NewSize(float procent = 1f)
    {
        // Angle the camera can see above the center.
        float halfFovRadians = cam.fieldOfView * Mathf.Deg2Rad / 2f;

        // How high is it from top to bottom of the view frustum,
        // in world space units, at our target depth?
        float visibleHeightAtDepth = procent * Mathf.Tan(halfFovRadians) * 2f;

        // You could also use Sprite.bounds for this.
        float spriteHeight = spriteRenderer.sprite.rect.height
                           / spriteRenderer.sprite.pixelsPerUnit;

        // How many times bigger (or smaller) is the height we want to fill?
        return  visibleHeightAtDepth / spriteHeight;
    }
    
}
