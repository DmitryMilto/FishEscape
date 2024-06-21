using Scripts.Card;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ControllerHealth : UIViewControllerBase
{
    [Inject]
    private HealthSystem _healthSystem;

    [SerializeField]
    private CardHealth prefabs;

    [SerializeField]
    private Transform container;

    [SerializeField]
    [ReadOnly]
    private List<CardHealth> ListHealth;
    protected override void Start()
    {
        base.Start();
        _healthSystem.onAddDamage += ActiveCard;
    }
    protected override void HideCallback()
    {
        //_healthSystem.onAddDamage -= OnAddDamageChanged;
        //_healthSystem.onAddHealth -= OnAddHealthChanged;
        Clear(container);
    }

    protected override void ShowCallback()
    {
        CreateHealthCard();
        this.AutoSizeWindow();
    }

    private void CreateHealthCard()
    {
        for(int i = 0; i < 5; i++)
        {
            var card = Instantiate(prefabs);
            card.CardShader.InitShader();
            card.transform.SetParent(container, false);
            ListHealth.Add(card);
        }
        InstanceCard(_healthSystem.health);
    }
    [Button]
    private void OnAddDamageChanged()
    {
        Debug.Log($"Add Damage {_healthSystem.health}");
        ActiveCard(_healthSystem.health);
    }
    [Button]
    private void OnAddHealthChanged()
    {
        Debug.Log($"Ok HP {_healthSystem.health}");
        ActiveCard(_healthSystem.health);
    }
    [Button]
    private void ActiveCard(int targetHealth)
    {
        Debug.Log($"Target Health {targetHealth}");
        for(int i = 0;i < ListHealth.Count; i++)
        {
            if(i < targetHealth)
            {
                if (!ListHealth[i].isActive)
                    ListHealth[i].Health();
            }
            else
            {
                if (ListHealth[i].isActive)
                    ListHealth[i].Damage();
            }
        }
    }
    private void InstanceCard(int targetHealth)
    {
        for (int i = 0; i < ListHealth.Count; i++)
        {
            if (i < targetHealth)
            {
                ListHealth[i].InstanceShow();
            }
            else
            {
                ListHealth[i].InstanceHide();
            }
        }
    }
}
