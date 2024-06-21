using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class HealthSystem
{
    public UnityAction<int> onAddDamage;

    public int health;
    public int maxHealth;

    public HealthSystem(int health)
    { 
        this.health = health;
        this.maxHealth = 5;
    }

    public void UpdateDamage(int operation)
    {
        var target = health + operation;
        health = Mathf.Clamp(target, 0, maxHealth);
        onAddDamage?.Invoke(this.health); 
    }
}
