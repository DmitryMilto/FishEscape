using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompany : MonoBehaviour {

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!LevelSceneController.gameOver)
            if (other.gameObject.tag == "shark")
            {
                LevelSceneController.xp -= 1;
            }
    }
}
