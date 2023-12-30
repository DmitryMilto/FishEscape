using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ScalerFish : MonoBehaviour
{
    private Camera cam
    {
        get => Camera.main;
    }
    private float sizeFish => GetComponent<EnemyMono>().Size;
    private Sprite sprite => GetComponent<EnemyMono>().Sprite;

    private float scale = 0.2f;
    private IEnumerator coroutine;
    private void OnEnable()
    {
        coroutine = Scale();
        StartCoroutine(coroutine);
    }
    private void OnDisable()
    {
        this.transform.DOKill();
        StopCoroutine(coroutine);
        Destroy(this);
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
            float spriteHeight = sprite.rect.height
                               / sprite.pixelsPerUnit;

            // How many times bigger (or smaller) is the height we want to fill?
            return visibleHeightAtDepth / spriteHeight;
        }
    }

    private void SetScale(float value)
    {
        this.transform.DOScale(NewSize + value, .5f).SetEase(Ease.Linear);
    }
}
