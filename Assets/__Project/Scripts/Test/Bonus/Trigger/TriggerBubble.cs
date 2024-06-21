using Scripts.Bonus.Enum;
using UnityEngine;

public class TriggerBubble : TriggerBase<EnumBonus>
{
    private void OnEnable()
    {
        initialize = GetComponent<InitializeBubble>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            initialize.Send();
            this.gameObject.SetActive(false);
        }
    }
}
