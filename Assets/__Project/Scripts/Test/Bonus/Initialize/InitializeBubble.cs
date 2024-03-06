using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class InitializeBubble : InitializeBase<EnumBonus>
{
    public BonusBubble bonusBubble { get; private set; }

    [Button]
    public override void Initialize(EmptyDatabaseBonus<EnumBonus> bonus)
    {
        bonusBubble = new BonusBubble(bonus.TypeBonus, this.healthSystem, this.speedSystem);
        bonusDB = bonus;

        this.Setting();
    }
    public override void Send()
    {
       this.bonusBubble.Send();
    }
}
