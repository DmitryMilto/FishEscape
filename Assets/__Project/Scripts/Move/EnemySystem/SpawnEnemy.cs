using FishEscape.Fishs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<EnemyFish> emenies;

    private PoolMono<InitializeEnemy> poolObject;
    [SerializeField] private InitializeEnemy prefabsEnemy;

    [SerializeField] private int countEnemy = 5;

    private IEnumerator coroutine;

    [SerializeField] private bool isPause = false;
    [SerializeField] private float secondSpawn = 2f;

    public void SetEnemy(List<EnemyFish> enemies)
    {
        if (emenies.Count == 0) return;
        this.emenies = enemies;
    }
    private void Start()
    {
        if (prefabsEnemy == null) return;
        poolObject = new PoolMono<InitializeEnemy>(prefabsEnemy, countEnemy, this.transform);
        coroutine = ISpawnEnemy();
        StartCoroutine(coroutine);
    }
    private IEnumerator ISpawnEnemy()
    {
        while (!isPause)
        {
            yield return new WaitForSeconds(secondSpawn);
            if (this.poolObject.GetFreeElement() != null)
                Enemy();
        }
    }
    private void Enemy()
    {
        var enemy = this.poolObject.GetFreeElement();
        var rand = emenies[Random.Range(0, emenies.Count)];
        enemy.Init(rand);
        enemy.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    internal void SetEnemy(object chooseEnemy)
    {
        throw new System.NotImplementedException();
    }
}
