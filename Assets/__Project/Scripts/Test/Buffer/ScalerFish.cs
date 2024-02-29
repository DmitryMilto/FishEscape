using DG.Tweening;
using Scripts.Buffer;
using System.Collections;
using UnityEngine;

public class ScalerFish : BaseBuffer, IBuffer
{
    private Camera cam
    {
        get => Camera.main;
    }
    
    private float sizeFish => Enemy.Size;
    private Sprite sprite => Enemy.Sprite;

    private float scale = 0.2f;
    
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

    public override IBuffer buffer => this;

    public IEnumerator UpdateBuffer()
    {
        while (true)
        {
            SetBuffer(scale);
            yield return new WaitForSeconds(time);
            SetBuffer(0f);
            yield return new WaitForSeconds(time);
        }
    }
    public void SetBuffer(float value)
    {
        this.transform.DOScale(NewSize + value, time).SetEase(Ease.Linear);
    }
    public void ResetBuffer()
    {
        this.transform.DOScale(NewSize, .1f).SetEase(Ease.Linear);
    }
}
