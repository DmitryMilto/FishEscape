using Scripts.Bonus.Enum;
using UnityEngine;

public class TriggerPuzzle : TriggerBase<EnumPuzzle>
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            initialize.Send();
            this.gameObject.SetActive(false);
        }
    }
}
