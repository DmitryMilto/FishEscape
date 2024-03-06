using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnBonus : BaseSpawnRun<EnumBonus>
{
    //[Inject]
    [SerializeField]
    private HealthSystem healthSystem;
    [Inject]
    [SerializeField]
    private SpeedSystem speedSystem;

    [SerializeField]
    private InitializeBubble prefabs;


    private PoolMono<InitializeBubble> poolBubble;
    private Coroutine coroutine;

    private void Start()
    {
        if (prefabs == null) return;
        poolBubble = new PoolMono<InitializeBubble>(prefabs, countBuble, this.transform);
        coroutine = StartCoroutine(SpawnBubble());
    }

    private IEnumerator SpawnBubble()
    {
        while (true)
        {
            var freeElement = poolBubble.GetFreeElement();
            if (freeElement != null)
            {
                freeElement.gameObject.SetActive(true);
                freeElement.Initialize(database[0]);

                freeElement.healthSystem = healthSystem;
                freeElement.speedSystem = speedSystem;
            }
            yield return null;
        }
    }
    private void OnDestroy()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
