using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCompany : MonoBehaviour {

    public Transform player;

    public static float speed;

    void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = mousePos.y > 3.26f ? 3.26f : mousePos.y;
        mousePos.y = mousePos.y < -4f ? -4f : mousePos.y;
        if (!LevelSceneController.gameOver)
            player.position = Vector2.MoveTowards(player.position, new Vector2(-2f, mousePos.y), Time.deltaTime * speed);
    }
}
