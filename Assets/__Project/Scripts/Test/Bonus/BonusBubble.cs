using Scripts.Bonus.Enum;

public class BonusBubble : BaseBonus<EnumBonus>
{
    public EnumBonus value { get; private set; }
    private HealthSystem healthSystem;
    private SpeedSystem speedSystem;

    public BonusBubble(EnumBonus value, HealthSystem healthSystem, SpeedSystem speedSystem)
    {
        this.value = value;
        this.healthSystem = healthSystem;
        this.speedSystem = speedSystem;
    }
    public void Send()
    {
        switch (value)
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
