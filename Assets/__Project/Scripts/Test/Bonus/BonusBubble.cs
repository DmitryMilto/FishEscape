using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using System;
using Zenject;

public class BonusBubble : BaseBonus<EnumBonus>
{
    [Inject]
    private HealthSystem healthSystem;
    [Inject]
    private SpeedSystem speedSystem;

    public override void Send()
    {
        switch (bonus)
        {
            case EnumBonus.Health:
                healthSystem.AddHealth();
                break;
            case EnumBonus.UpSpeed:
                speedSystem.AddSpeed(5);
                break;
            case EnumBonus.DownSpeed:
                speedSystem.RemoveSpeed(5);
                break;
            case EnumBonus.Bomb:
                healthSystem.AddDamage(2);
                break;
            default:
                break;
        }
    }
}
