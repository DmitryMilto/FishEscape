using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemies : MonoBehaviour {

    public GameObject[] adversary;
    public int start, end;
    public static int level;
    public static float time;

    private bool game;

    public void Awake()
    {
        SelectEnemy(level);
        StartCoroutine(Enemy());
    }

    IEnumerator Enemy()
    {
        while (!LevelSceneController.gameOver && !LevelSceneController.pause)
        {
            Instantiate(adversary[Random.Range(start, end)], new Vector2(10f, Random.Range(-4f, 3.26f)), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
    private void SelectEnemy(int level)
    {
        switch (level)
        {
            case 1: start = 0; end = 3; break;
            case 2: start = 3; end = 5; break;
            case 3: start = 5; end = 7; break;
            case 4: start = 7; end = 9; break;
            case 5: start = 9; end = 10; break;
        }
    }
}
