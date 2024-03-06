using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BubbleScale : MonoBehaviour
{
    private float _size;
    public float size { get { return _size; } set { _size = value; UpdateScale(); } }
    private Sprite sprite;
    private Sprite Sprite
    {
        get
        {
            if (sprite == null)
                sprite = GetComponentInChildren<SpriteRenderer>().sprite;
            return sprite;
        }
    }
    private Camera cam
    {
        get => Camera.main;
    }
    private float NewSize
    {
        get
        {
            // Angle the camera can see above the center.
            float halfFovRadians = cam.fieldOfView * Mathf.Deg2Rad / 2f;

            // How high is it from top to bottom of the view frustum,
            // in world space units, at our target depth?
            float visibleHeightAtDepth = this.size * Mathf.Tan(halfFovRadians) * 2f;

            // You could also use Sprite.bounds for this.
            float spriteHeight = Sprite.rect.height
                               / Sprite.pixelsPerUnit;

            // How many times bigger (or smaller) is the height we want to fill?
            return visibleHeightAtDepth / spriteHeight;
        }
    }
    [Button]
    public void UpdateScale()
    {
        this.transform.DOScale(NewSize, .1f).SetEase(Ease.Linear);
    }
}
