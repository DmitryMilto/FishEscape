using FishEscape.Fishs;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEnemy : MonoBehaviour
{
    [SerializeField] private List<EnemyFish> emenies;

    private PoolMono<EnemyMono> poolObject;
    [SerializeField] private EnemyMono prefabsEnemy;
    [SerializeField] private int countEnemy = 5;

    private IEnumerator coroutine;

    [SerializeField] private bool isPause = false;
    [SerializeField] private float secondSpawn = 2f;
    private void Start()
    {
        if (prefabsEnemy == null) return;
        poolObject = new PoolMono<EnemyMono>(prefabsEnemy, countEnemy, this.transform);
        coroutine = SpawnEnemy();
        StartCoroutine(coroutine);
    }
    [Button]
    private void Enemy()
    {
        var enemy = this.poolObject.GetFreeElement();
        var rand = emenies[Random.Range(0, emenies.Count)];
        enemy.Init(rand.fish, rand.speed, rand.sizeFish, rand.FlipX);
        enemy.gameObject.SetActive(true);
    }

    private IEnumerator SpawnEnemy()
    {
        while (!isPause)
        {
            yield return new WaitForSeconds(secondSpawn);
            if (this.poolObject.GetFreeElement() != null)
                Enemy();
        }
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
