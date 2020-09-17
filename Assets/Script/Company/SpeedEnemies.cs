using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEnemies : MonoBehaviour {

    [SerializeField]
    public static float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(Random.Range(5f, 15f) * Time.deltaTime, 0, 0);
        if (!LevelSceneController.gameOver)
            if (transform.position.x < -10f)
            {
                Destroy(gameObject);
                LevelSceneController.score += 1;
            }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!LevelSceneController.gameOver)
            if (other.gameObject.tag == "player")
            {
                LevelSceneController.score += 1;
                Destroy(gameObject);
            }
    }
}
