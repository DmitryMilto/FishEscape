using Scripts.Bonus.Enum;
using UnityEngine;

public class ControllerBubble : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer bubble;
    [SerializeField]
    private SpriteRenderer element;

    public void SetImage(Sprite buffer, TypeBubble isBubble)
    {
        this.element.sprite = buffer;
        bubble.enabled = isBubble == TypeBubble.WithBubble ? true : false;
    }
}
