using Scripts.Bonus.Database;
using System;
using UnityEngine;
using Zenject;

public abstract class InitializeBase<T> : MonoBehaviour where T : Enum
{
    [Inject]
    public HealthSystem healthSystem;
    [Inject]
    public SpeedSystem speedSystem;

    [SerializeField]
    private ControllerBubble controller;
    public ControllerBubble Controller { get 
        {
            if(controller == null) controller = GetComponent<ControllerBubble>();
            return controller;
        }
        private set { controller = value; } }

    [SerializeField]
    private BubbleScale scale;
    public BubbleScale Scale
    {
        get
        {
            if (controller == null) scale = GetComponent<BubbleScale>();
            return scale;
        }
        private set { scale = value; }
    }

    [SerializeField]
    protected EmptyDatabaseBonus<T> bonusDB;

    private void Start()
    {
        
        scale = GetComponent<BubbleScale>();
    }

    public abstract void Initialize(EmptyDatabaseBonus<T> bonus);
    public abstract void Send();

    protected void Setting()
    {
        if (bonusDB != null)
        {
            this.Controller.SetImage(bonusDB.element, bonusDB.TypeBubble);
            this.Scale.size = bonusDB.scale;
        }
    }
}
