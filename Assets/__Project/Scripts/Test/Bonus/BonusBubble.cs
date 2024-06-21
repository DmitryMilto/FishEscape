using Scripts.Bonus.Enum;
using UnityEngine;

public class BonusBubble : BaseBonus<EnumBonus>
{
    public EnumBonus value { get; private set; }

    public BonusBubble(EnumBonus value)
    {
        this.value = value;
        //this.healthSystem = healthSystem;
        //this.speedSystem = speedSystem;
    }
    public void Send()
    {
        //switch (value)
        //{
        //    case EnumBonus.Health:
        //        healthSystem.UpdateDamage(1);
        //        break;
        //    case EnumBonus.UpSpeed:
        //        speedSystem.AddSpeed(5);
        //        break;
        //    case EnumBonus.DownSpeed:
        //        speedSystem.RemoveSpeed(5);
        //        break;
        //    case EnumBonus.Bomb:
        //        healthSystem.UpdateDamage(-2);
        //        break;
        //    default:
        //        break;
        //}
    }
}
