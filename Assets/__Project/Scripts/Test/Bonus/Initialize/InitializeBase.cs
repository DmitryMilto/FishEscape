using Scripts.Bonus.Database;
using System;
using UnityEngine;
using Zenject;

public abstract class InitializeBase<T> : MonoBehaviour where T : Enum
{
    public HealthSystem healthSystem { get; set; }
    public SpeedSystem speedSystem { get; set; }

    public ControllerBubble controller { get; private set; }
    public BubbleScale scale { get; private set; }

    [SerializeField]
    protected EmptyDatabaseBonus<T> bonusDB;

    private void Start()
    {
        controller = GetComponent<ControllerBubble>();
        scale = GetComponent<BubbleScale>();
    }

    public abstract void Initialize(EmptyDatabaseBonus<T> bonus);
    public abstract void Send();

    protected void Setting()
    {
        this.controller.SetImage(bonusDB.element, bonusDB.TypeBubble);
        this.scale.size = bonusDB.scale;
    }
}
