using System;
using UnityEngine;

[Serializable]
public class HealthSystem
{
    public delegate void AddDamageDelegate();
    public AddDamageDelegate onAddDamage;

    public delegate void AddHealthDelegate();
    public AddHealthDelegate onAddHealth;
    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public HealthSystem(int health)
    { 
        this.health = health;
        this.maxHealth = 5;
    }

    public void AddDamage(int damage = 1)
    {
        var _ = health - damage;
        health = Mathf.Clamp(_, 0, maxHealth);
        onAddDamage?.Invoke(); 
    }
    public void AddHealth(int health = 1)
    {
        var _ = this.health + health;
        this.health = Mathf.Clamp(_, 0, maxHealth);
        onAddHealth?.Invoke();
    }
}
